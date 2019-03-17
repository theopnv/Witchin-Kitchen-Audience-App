using System.Collections;
using audience.messages;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace audience.game
{

    public class GameManager : MonoBehaviour
    {
        private NetworkManager _NetworkManager;

        [SerializeField] private GameObject _OneChoiceOverlayPrefab;
        [SerializeField] private GameObject _TwoChoicesOverlayPrefab;
        [SerializeField] private Canvas _Canvas;

        [SerializeField] private GameObject _PrimaryPanelPrefab;
        [SerializeField] private GameObject _PollPanelPrefab;
        [SerializeField] private GameObject _ExitRoomPanelPrefab;
        [SerializeField] private GameObject _SpellsPanelPrefab;

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
            }

            var primaryPanel = Instantiate(_PrimaryPanelPrefab, _Canvas.transform).GetComponent<PrimaryPanelManager>();
            primaryPanel.GameManager = this;
        }

        void OnDisable()
        {
            _NetworkManager.OnDisconnected -= OnDisconnectedFromServer;
            _NetworkManager.OnMessageReceived -= OnMessageReceivedFromServer;

            _NetworkManager.OnReceivedPollList -= OnReceivedPollList;
            _NetworkManager.OnReceivedGameOutcome -= OnReceivedGameOutcome;
            _NetworkManager.OnReceivedSpellRequest -= OnReceivedSpellRequest;
            _NetworkManager.OnReceivedEndGame -= OnReceiveEndGame;

        }

        #endregion

        #region Custom Methods

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
                switch (content.code)
                {
                }
            }
            else
            {
                Debug.LogError(content.content);
                switch (content.code)
                {
                    case Code.error_vote_didnt_pass:
                        break;
                    default: break;
                }
            }
        }

        void OnReceivedPollList(PollChoices pollChoices)
        {
            var pollManager = Instantiate(_PollPanelPrefab, _Canvas.transform).GetComponent<PollPanelManager>();
            pollManager.PollChoices = pollChoices;
        }

        void OnReceivedGameOutcome(GameOutcome gameOutcome)
        {
            var gameOutcomeManager = 
                Instantiate(_ExitRoomPanelPrefab, _Canvas.transform)
                    .GetComponent<GameOutcomePanelManager>();
            gameOutcomeManager.GameOutcome = gameOutcome;
        }

        void OnReceivedSpellRequest()
        {
            var spellManager = Instantiate(_SpellsPanelPrefab, _Canvas.transform).GetComponent<SpellsPanelManager>();
            spellManager.AuthorizeCasting = true;
        }

        void OnReceiveEndGame(EndGame endGame)
        {
            if (endGame.doRematch)
            {
                for (var i = 0; i < GameInfo.PlayerNumber; i++)
                {
                    GameInfo.PlayerScores[i] = 0;
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
        
        #endregion

    }

}
