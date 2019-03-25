using UnityEngine;
using UnityEngine.SceneManagement;

namespace audience.tutorial
{
    public class EndPanelManager : ATutorialPanelManager
    {
        [SerializeField] private GameObject _EventsPanel;

        #region Custom Methods


        public void NextPage()
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
            if (GameInfo.InGame)
            {
                SceneManager.LoadSceneAsync(SceneNames.Game);
            }
            else
            {
                SceneManager.LoadSceneAsync(SceneNames.TitleScreen);
            }
        }

        public void PreviousPage()
        {
            Instantiate(_EventsPanel, _Canvas.transform);
            Destroy(gameObject);
        }

        #endregion
    }
}