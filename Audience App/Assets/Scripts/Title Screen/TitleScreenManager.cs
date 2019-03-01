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
        [SerializeField] private GameObject _TwoChoicesOverlayPrefab;
        [SerializeField] private Canvas _Canvas;

        void Start()
        {
            QualitySettings.vSyncCount = 1;

            // Setting default values in case this is the first time the app is started
            if (!PlayerPrefs.HasKey(Key.HOST_ADDRESS))
            {
                PlayerPrefs.SetString(Key.HOST_ADDRESS, "http://dev.audience.witchin-kitchen.com/");
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                var instance = Instantiate(_TwoChoicesOverlayPrefab, _Canvas.transform);
                var manager = instance.GetComponent<Overlay>();
                manager.Primary += Application.Quit;
                manager.Secondary += () => { Destroy(manager.gameObject); };
                manager.Description = StringLitterals.EXIT_APP_CONFIRMATION;
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
