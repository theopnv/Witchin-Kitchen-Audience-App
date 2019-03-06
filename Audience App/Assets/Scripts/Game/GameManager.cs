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
            var instance = Instantiate(_PrimaryPanelPrefab, _Canvas.transform);
            instance.GetComponent<PrimaryPanelManager>().GameManager = this;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                var instance = Instantiate(_TwoChoicesOverlayPrefab, _Canvas.transform);
                var manager = instance.GetComponent<Overlay>();
                manager.Primary += ExitRoom;
                manager.Secondary += () => { Destroy(manager.gameObject); };
                manager.Description = StringLitterals.RETURN_TO_TITLE_SCREEN_CONFIRMATION;
            }
        }

        #endregion

        #region Custom Methods

        public void ExitRoom()
        {
            _NetworkManager?.ExitRoom();
            Destroy(_NetworkManager.gameObject);
            SceneManager.LoadSceneAsync(SceneNames.TitleScreen);
        }

        public void StartPoll(PollChoices pollChoices)
        {
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
