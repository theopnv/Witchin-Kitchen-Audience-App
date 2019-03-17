using System.Collections;
using System.Collections.Generic;
using audience;
using audience.messages;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace audience.game
{
    public class PrimaryPanelManager : APanelManager
    {
        [HideInInspector] public GameManager GameManager;

        private NetworkManager _NetworkManager;
        private Canvas _Canvas;
        [SerializeField] private GameObject _TwoChoicesOverlayPrefab;
        [SerializeField] private GameObject _SpellsManagerPrefab;

        [SerializeField] private GameObject _PlayersStatePlaceholder;
        [SerializeField] private GameObject _PlayerStatePrefab;
        private Dictionary<int, PlayerStateManager> _PlayerStateManagers;

        void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            _Canvas = FindObjectOfType<Canvas>();
            _NetworkManager = FindObjectOfType<NetworkManager>();
            if (_NetworkManager)
            {
                _NetworkManager.OnReceivedGameStateUpdate += OnGameStateUpdate;
            }

            _PlayerStateManagers = new Dictionary<int, PlayerStateManager>();
            for (var i = 0; i < GameInfo.PlayerNumber; i++)
            {
                var instance = Instantiate(_PlayerStatePrefab, _PlayersStatePlaceholder.transform);
                var manager = instance.GetComponent<PlayerStateManager>();
                manager.Score = GameInfo.PlayerPotions[i];
                manager.Name = GameInfo.PlayerNames[i];

                _PlayerStateManagers.Add(i, manager);
            }
        }

        void OnDisable()
        {
            _NetworkManager.OnReceivedGameStateUpdate -= OnGameStateUpdate;
        }

        public override void ExitScreen()
        {
            var instance = Instantiate(_TwoChoicesOverlayPrefab, _Canvas.transform);
            var manager = instance.GetComponent<Overlay>();
            manager.Primary += ExitRoom;
            manager.Secondary += () => { Destroy(manager.gameObject); };
            manager.Description = StringLitterals.RETURN_TO_TITLE_SCREEN_CONFIRMATION;
        }

        public void BrowseSpells()
        {
            var spellManager = Instantiate(_SpellsManagerPrefab, _Canvas.transform).GetComponent<SpellsPanelManager>();
            spellManager.AuthorizeCasting = false;
        }

        public void ExitRoom()
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
            _NetworkManager?.ExitRoom();
            Destroy(_NetworkManager?.gameObject);
            SceneManager.LoadSceneAsync(SceneNames.TitleScreen);
        }

        public void OnGameStateUpdate(Game game)
        {
            for (var i = 0; i < GameInfo.PlayerNumber; i++)
            {
                _PlayerStateManagers[i].Score = GameInfo.PlayerPotions[i];
                _PlayerStateManagers[i].Name = GameInfo.PlayerNames[i];
            }
        }
    }

}
