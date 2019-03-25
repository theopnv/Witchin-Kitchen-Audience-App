using UnityEngine;
using UnityEngine.SceneManagement;

namespace audience.tutorial
{
    public class ATutorialPanelManager : APanelManager
    {
        protected Canvas _Canvas;
        [SerializeField] private GameObject _TwoChoicesOverlayPrefab;

        protected virtual void Start()
        {
            _Canvas = FindObjectOfType<Canvas>();
        }

        void OnDisable()
        {
        }

        public void OnExitButtonClick()
        {
            var overlay = Instantiate(_TwoChoicesOverlayPrefab, _Canvas.transform).GetComponent<Overlay>();
            overlay.Primary = () => ExitRoom();
            overlay.Secondary = () => Destroy(overlay.gameObject);
            overlay.Description = StringLitterals.EXIT_TUTORIAL_CONFIRMATION;
        }

        public void ExitRoom()
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
            SceneManager.LoadSceneAsync(GameInfo.InGame ? SceneNames.Game : SceneNames.TitleScreen);
        }
    }

}
