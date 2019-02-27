using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SocketIO;
using Key = audience.PlayerPrefsKeys;

namespace audience.title_screen
{

    public class TitleScreenManager : MonoBehaviour
    {
        void Start()
        {
            QualitySettings.vSyncCount = 1;

            // Setting default values in case this is the first time the app is started
            if (!PlayerPrefs.HasKey(Key.HOST_ADDRESS))
            {
                PlayerPrefs.SetString(Key.HOST_ADDRESS, "http://dev.witchin-kitchen.com/");
            }
        }

        public void OnMainButtonClick()
        {
            SceneManager.LoadSceneAsync(SceneNames.Lobby);
        }

        public void OnSettingsButtonClick()
        {
            SceneManager.LoadSceneAsync(SceneNames.Settings);
        }
    }

}
