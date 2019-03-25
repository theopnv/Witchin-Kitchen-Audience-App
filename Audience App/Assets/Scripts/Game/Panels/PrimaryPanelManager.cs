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
            if (_NetworkManager)
            {
                _NetworkManager.OnReceivedGameStateUpdate -= OnGameStateUpdate;
            }
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

        public void OnExitButtonClick()
        {
            var overlay = Instantiate(_TwoChoicesOverlayPrefab, _Canvas.transform).GetComponent<Overlay>();
            overlay.Primary = () => ExitRoom();
            overlay.Secondary = () => Destroy(overlay.gameObject);
            overlay.Description = "Do you really want to stop watching Witchin Kitchen ?";
        }

        public void OnHelpButtonClick()
        {

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
                var oldScore = _PlayerStateManagers[i].Score;
                if (oldScore != GameInfo.PlayerPotions[i])
                {
                    _PlayerStateManagers[i].PlusOneImage.gameObject.SetActive(true);
                    StartCoroutine(SetPlusOneActiveFalse(_PlayerStateManagers[i]));
                }
                _PlayerStateManagers[i].Score = GameInfo.PlayerPotions[i];
                _PlayerStateManagers[i].Name = GameInfo.PlayerNames[i];
            }
        }

        private IEnumerator SetPlusOneActiveFalse(PlayerStateManager p)
        {
            yield return new WaitForSeconds(2);
            p.PlusOneImage.CrossFadeAlpha(0, 2, false);
            yield return new WaitForSeconds(2);
            p.PlusOneImage.gameObject.SetActive(false);
            p.PlusOneImage.color = new Color(p.PlusOneImage.color.r, p.PlusOneImage.color.g, p.PlusOneImage.color.b, 255);
        }
    }

}
