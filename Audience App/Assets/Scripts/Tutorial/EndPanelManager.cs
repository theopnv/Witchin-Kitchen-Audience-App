using UnityEngine;
using UnityEngine.SceneManagement;

namespace audience.tutorial
{
    public class EndPanelManager : ATutorialPanelManager
    {
        [SerializeField] private GameObject _ThemeIngredientPanel;

        #region Custom Methods


        public void NextPage()
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
            SceneManager.LoadSceneAsync(SceneNames.TitleScreen);
        }

        public void PreviousPage()
        {
            Instantiate(_ThemeIngredientPanel, _Canvas.transform).GetComponent<ThemeIngredientPanelManager>();
            Destroy(gameObject);
        }

        #endregion
    }
}