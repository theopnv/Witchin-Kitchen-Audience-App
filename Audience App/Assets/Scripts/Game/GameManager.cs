using System.Collections;
using System.Collections.Generic;
using audience;
using audience.messages;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace audience.game
{

    public class GameManager : MonoBehaviour
    {
        private NetworkManager _NetworkManager;
        private bool IsOnPrimaryPanel;

        [SerializeField] private GameObject _OneChoiceOverlayPrefab;
        [SerializeField] private GameObject _TwoChoicesOverlayPrefab;
        [SerializeField] private Canvas _Canvas;

        [SerializeField] private GameObject _PrimaryPanelPrefab;

        [SerializeField] private GameObject _PollPanelPrefab;
        [HideInInspector] public PollPanelManager PollPanelManager;

        [SerializeField] private GameObject _ExitRoomPanelPrefab;

        [SerializeField] private GameObject _SpellsPanelPrefab;
        [HideInInspector] public SpellsPanelManager SpellsPanelManager;

        #region Unity API

        // Start is called before the first frame update
        void Start()
        {
            _NetworkManager = FindObjectOfType<NetworkManager>();
            _NetworkManager.OnDisconnected += OnDisconnectedFromServer;
            _NetworkManager.OnMessageReceived += OnMessageReceivedFromServer;

            var instance = Instantiate(_PrimaryPanelPrefab, _Canvas.transform);
            instance.GetComponent<PrimaryPanelManager>().GameManager = this;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (IsOnPrimaryPanel)
                {
                    var instance = Instantiate(_TwoChoicesOverlayPrefab, _Canvas.transform);
                    var manager = instance.GetComponent<Overlay>();
                    manager.Primary += ExitRoom;
                    manager.Secondary += () => { Destroy(manager.gameObject); };
                    manager.Description = StringLitterals.RETURN_TO_TITLE_SCREEN_CONFIRMATION;
                }
                else
                {
                    DestroyLastPanel();
                }
            }
        }

        void OnDisable()
        {
            _NetworkManager.OnDisconnected -= OnDisconnectedFromServer;
            _NetworkManager.OnMessageReceived -= OnMessageReceivedFromServer;
        }

        #endregion

        #region Custom Methods

        public void DestroyLastPanel()
        {
            IsOnPrimaryPanel = false;
            if (PollPanelManager != null)
            {
                Destroy(PollPanelManager.gameObject);
                PollPanelManager = null;
            }

            if (SpellsPanelManager != null)
            {
                Destroy(SpellsPanelManager.gameObject);
                SpellsPanelManager = null;
            }
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
                switch (content.code)
                {
                    case Code.success_vote_accepted:
                        DestroyLastPanel();
                        break;
                    case Code.spell_casted_success:
                        DestroyLastPanel();
                        break;
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

        public void ExitRoom()
        {
            _NetworkManager?.ExitRoom();
            Destroy(_NetworkManager?.gameObject);
            SceneManager.LoadSceneAsync(SceneNames.TitleScreen);
        }

        public void StartPoll(PollChoices pollChoices)
        {
            Handheld.Vibrate();
            var pollPanel = Instantiate(_PollPanelPrefab, _Canvas.transform);
            PollPanelManager = pollPanel.GetComponent<PollPanelManager>();
            PollPanelManager.StartPoll(pollChoices, _NetworkManager);
        }

        public void SetGameOutcome(GameOutcome gameOutcome)
        {
            var exitRoomPanel = Instantiate(_ExitRoomPanelPrefab, _Canvas.transform);
            var exitRoomPanelManager = exitRoomPanel.GetComponent<ExitRoomPanelManager>();
            exitRoomPanelManager.SetOutcome(gameOutcome);
        }

        public void StartSpellSelection()
        {
            var spellPanel = Instantiate(_SpellsPanelPrefab, _Canvas.transform);
            SpellsPanelManager = spellPanel.GetComponent<SpellsPanelManager>();
            SpellsPanelManager.GenerateSpellCards(_NetworkManager);
        }

        #endregion

    }

}
