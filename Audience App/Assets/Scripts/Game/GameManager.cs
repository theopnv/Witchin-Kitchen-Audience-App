using System.Collections;
using audience.messages;
using audience.tutorial;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace audience.game
{

    public class GameManager : MonoBehaviour
    {
        [HideInInspector]
        public bool IsChoosingASpell = false;
        private NetworkManager _NetworkManager;

        [SerializeField] private GameObject _OneChoiceOverlayPrefab;
        [SerializeField] private GameObject _TwoChoicesOverlayPrefab;
        [SerializeField] private Canvas _Canvas;

        [SerializeField] private GameObject _PrimaryPanelPrefab;
        [SerializeField] private GameObject _PollPanelPrefab;
        [SerializeField] private GameObject _GameOutcomePanelPrefab;
        [SerializeField] private GameObject _SpellsPanelPrefab;
        [SerializeField] private GameObject _IngredientPollPrefab;

        private bool _GameIsAboutToEnd = false;

        #region Unity API

        // Start is called before the first frame update
        void Start()
        {
            _NetworkManager = FindObjectOfType<NetworkManager>();
            if (_NetworkManager)
            {
                _NetworkManager.OnDisconnected += OnDisconnectedFromServer;
                _NetworkManager.OnMessageReceived += OnMessageReceivedFromServer;

                _NetworkManager.OnReceivedPollList += OnReceivedPollList;
                _NetworkManager.OnReceivedGameOutcome += OnReceivedGameOutcome;
                _NetworkManager.OnReceivedSpellRequest += OnReceivedSpellRequest;
                _NetworkManager.OnReceivedEndGame += OnReceiveEndGame;

                _NetworkManager.OnReceivedVoteForIngredient += OnReceivedVoteForIngredient;
                _NetworkManager.OnReceivedIngredientPollResults += OnReceivedIngredientPoll;
                _NetworkManager.OnReceivedStopIngredientPoll += InstantiatePrimaryPanel;
            }

            if (TransmitIngredientPoll.WasAskedToVote)
            {
                InstantiateIngredientPollPanel();
            }
            else
            {
                InstantiatePrimaryPanel();
            }
        }

        void OnDisable()
        {
            if (_NetworkManager)
            {
                _NetworkManager.OnDisconnected -= OnDisconnectedFromServer;
                _NetworkManager.OnMessageReceived -= OnMessageReceivedFromServer;

                _NetworkManager.OnReceivedPollList -= OnReceivedPollList;
                _NetworkManager.OnReceivedGameOutcome -= OnReceivedGameOutcome;
                _NetworkManager.OnReceivedSpellRequest -= OnReceivedSpellRequest;
                _NetworkManager.OnReceivedEndGame -= OnReceiveEndGame;

                _NetworkManager.OnReceivedVoteForIngredient -= OnReceivedVoteForIngredient;
                _NetworkManager.OnReceivedIngredientPollResults -= OnReceivedIngredientPoll;
                _NetworkManager.OnReceivedStopIngredientPoll -= InstantiatePrimaryPanel;
            }
        }

        #endregion

        #region Custom Methods

        void InstantiatePrimaryPanel()
        {
            var primaryPanel = Instantiate(_PrimaryPanelPrefab, _Canvas.transform).GetComponent<PrimaryPanelManager>();
            primaryPanel.GameManager = this;
        }

        void InstantiateIngredientPollPanel()
        {
            var pollPanel = Instantiate(_IngredientPollPrefab, _Canvas.transform).GetComponent<ThemeIngredientPanelManager>();
            pollPanel.IngredientPoll = TransmitIngredientPoll.Instance;
        }

        void OnDisconnectedFromServer()
        {
            var instance = Instantiate(_OneChoiceOverlayPrefab, _Canvas.transform);
            var errorOverlay = instance.GetComponent<Overlay>();
            errorOverlay.Description = "Disconnected from server. Please try joining the room again.";
            errorOverlay.Primary += () =>
            {
                _NetworkManager?.ExitRoom();
                SceneManager.LoadSceneAsync(SceneNames.TitleScreen);
            };
        }

        private void OnMessageReceivedFromServer(Base content)
        {
            if ((int)content.code % 10 == 0) // Success codes always have their unit number equal to 0 (cf. protocol)
            {
                Debug.Log(content.code + ": " + content.content);
            }
            else
            {
                Debug.LogError(content.content);
            }
        }

        void OnReceivedPollList(PollChoices pollChoices)
        {
            if (!_GameIsAboutToEnd)
            {
                var pollManager = Instantiate(_PollPanelPrefab, _Canvas.transform).GetComponent<PollPanelManager>();
                pollManager.PollChoices = pollChoices;
                pollManager.WasChoosingASpell = IsChoosingASpell;
            }
        }

        void OnReceivedGameOutcome(GameOutcome gameOutcome)
        {
            var gameOutcomeManager = 
                Instantiate(_GameOutcomePanelPrefab, _Canvas.transform)
                    .GetComponent<GameOutcomePanelManager>();
            gameOutcomeManager.GameOutcome = gameOutcome;
            _GameIsAboutToEnd = true;
        }

        void OnReceivedSpellRequest(SpellRequest spellRequest)
        {
            if (!_GameIsAboutToEnd && !IsChoosingASpell)
            {
                var spellManager = Instantiate(_SpellsPanelPrefab, _Canvas.transform).GetComponent<SpellsPanelManager>();
                var title = spellRequest.fromPlayer.id == -1
                    ? "A free potion has been offered to you!"
                    : spellRequest.fromPlayer.name + " made a potion for you!";
                spellManager.Title = title;
                spellManager.AuthorizeCasting = true;
                IsChoosingASpell = true;
            }
        }

        void OnReceiveEndGame(EndGame endGame)
        {
            if (endGame.doRematch)
            {
                for (var i = 0; i < GameInfo.PlayerNumber; i++)
                {
                    GameInfo.PlayerPotions[i] = 0;
                }

                SceneManager.LoadSceneAsync(SceneNames.Game);
            }
            else
            {
                var instance = Instantiate(_OneChoiceOverlayPrefab, _Canvas.transform);
                var errorOverlay = instance.GetComponent<Overlay>();
                errorOverlay.Description = "The cooking show is over. Witchin' Kitchen returns live each time new candidates are ready to risk their lives!";
                errorOverlay.Primary += () =>
                {
                    _NetworkManager?.ExitRoom();
                    SceneManager.LoadSceneAsync(SceneNames.TitleScreen);
                };
            }
        }

        void OnReceivedVoteForIngredient(IngredientPoll ingredientPoll)
        {
            TransmitIngredientPoll.Instance = ingredientPoll;
            InstantiateIngredientPollPanel();
        }

        void OnReceivedIngredientPoll(IngredientPoll ingredientPoll)
        {
            TransmitIngredientPoll.Instance = ingredientPoll;
        }

        #endregion

    }

}
