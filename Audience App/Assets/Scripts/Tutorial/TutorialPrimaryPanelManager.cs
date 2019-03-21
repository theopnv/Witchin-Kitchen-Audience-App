using System.Collections;
using System.Collections.Generic;
using audience;
using audience.messages;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace audience.game
{
    public class TutorialPrimaryPanelManager : ATutorialPanelManager
    {
        private Canvas _Canvas;
        [SerializeField] private GameObject _TwoChoicesOverlayPrefab;
        [SerializeField] private GameObject _SpellsManagerPrefab;

        void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            _Canvas = FindObjectOfType<Canvas>();
        }

        void OnDisable()
        {
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

        public void NextPage()
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

        public void ExitRoom()
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
            SceneManager.LoadSceneAsync(SceneNames.TitleScreen);
        }
    }

}
