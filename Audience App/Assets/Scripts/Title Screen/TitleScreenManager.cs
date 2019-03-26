using System.Collections;
using System.Collections.Generic;
using audience.messages;
using UnityEngine;
using UnityEngine.SceneManagement;
using SocketIO;
using Key = audience.PlayerPrefsKeys;

namespace audience.title_screen
{

    public class TitleScreenManager : MonoBehaviour
    {
        [SerializeField] private GameObject _TwoChoicesOverlayPrefab;
        [SerializeField] private GameObject _JoinButton;
        [SerializeField] private Canvas _Canvas;

        //Effects
        private Effects effects;

        void Start()
        {
            effects = new Effects(2, 0.9f, 0.9f);
            QualitySettings.vSyncCount = 1;

            ResetStaticVars();

            // Setting default values in case this is the first time the app is started
            if (!PlayerPrefs.HasKey(Key.HOST_ADDRESS))
            {
                PlayerPrefs.SetString(Key.HOST_ADDRESS, "http://dev.audience.witchin-kitchen.com/");
            }

            var networkManager = GameObject.FindObjectOfType<NetworkManager>();
            if (networkManager)
            {
                Destroy(networkManager.gameObject);
            }
        }

        private void ResetStaticVars()
        {
            GameInfo.PlayerNumber = 0;
            GameInfo.PlayerIDs = new int[4];
            GameInfo.PlayerColors = new Color[4];
            GameInfo.PlayerNames = new string[4];
            GameInfo.PlayerPotions = new int[4];
            GameInfo.PlayerIngredients = new int[4];
            GameInfo.InGame = false;

            ViewerInfo.SocketId = "";
            ViewerInfo.Name = "";

            TransmitIngredientPoll.Instance = null;
            TransmitIngredientPoll.Voted = false;
            TransmitIngredientPoll.WasAskedToVote = false;
        }

        void Update()
        {
            effects.GrowShrink(_JoinButton.transform);
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

        public void OnTutorialButtonClick()
        {
            SceneManager.LoadSceneAsync(SceneNames.Tutorial);
        }
    }

}
