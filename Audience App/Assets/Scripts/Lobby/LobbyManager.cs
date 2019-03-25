using System;
using audience.messages;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WebSocketSharp;

namespace audience.lobby
{

    public class LobbyManager : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField]
        private InputField _RoomPinInputField;

        [SerializeField]
        private InputField _NameInputField;

        [SerializeField]
        private Canvas _Canvas;

        [SerializeField]
        private GameObject _ErrorOverlayPrefab;

        private NetworkManager _NetworkManager;

        #endregion

        #region Unity API

        void Start()
        {
            _NetworkManager = FindObjectOfType<NetworkManager>();
            _NetworkManager.OnMessageReceived += OnMessageReceivedFromServer;
            _NetworkManager.OnReceivedVoteForIngredient += OnReceivedVoteForIngredient;

            InitGameInfo();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadSceneAsync(SceneNames.TitleScreen);
            }
        }

        void OnDisable()
        {
            _NetworkManager.OnMessageReceived -= OnMessageReceivedFromServer;
            _NetworkManager.OnReceivedVoteForIngredient -= OnReceivedVoteForIngredient;
        }

        #endregion

        #region Custom Methods

        private void InitGameInfo()
        {
            GameInfo.PlayerNumber = 4;
            GameInfo.PlayerIDs = new int[GameInfo.PlayerNumber];
            GameInfo.PlayerColors = new Color[GameInfo.PlayerNumber];
            GameInfo.PlayerNames = new string[GameInfo.PlayerNumber];
            GameInfo.PlayerPotions = new int[GameInfo.PlayerNumber];
            GameInfo.PlayerIngredients = new int[GameInfo.PlayerNumber];
        }

        private void InstantiateErrorOverlay(string error)
        {
            var instance = Instantiate(_ErrorOverlayPrefab, _Canvas.transform);
            var errorOverlay = instance.GetComponent<Overlay>();
            errorOverlay.Description = error;
            errorOverlay.Primary += () => { Destroy(instance.gameObject); };
        }

        public void OnJoinButtonClick()
        {
            if (_NameInputField.text.IsNullOrEmpty())
            {
                InstantiateErrorOverlay(StringLitterals.ERROR_NO_NAME);
                return;
            }
            ViewerInfo.Name = _NameInputField.text;

            if (_RoomPinInputField.text.IsNullOrEmpty())
            {
                InstantiateErrorOverlay(StringLitterals.ERROR_NO_PIN);
                return;
            }

            if (!_NetworkManager.IsConnectedToServer)
            {
                InstantiateErrorOverlay(StringLitterals.ERROR_SERVER_UNREACHABLE);
            }

            try
            {
                var asInt = int.Parse(_RoomPinInputField.text);
                _NetworkManager.EmitJoinGame(asInt);
            }
            catch (Exception e)
            {
                InstantiateErrorOverlay(StringLitterals.ERROR_WRONG_PIN);
            }
        }

        public void OnBackButtonClick()
        {
            SceneManager.LoadSceneAsync(SceneNames.TitleScreen);
        }

        void OnReceivedVoteForIngredient(IngredientPoll ingredientPoll)
        {
            TransmitIngredientPoll.Instance = ingredientPoll;
            TransmitIngredientPoll.WasAskedToVote = true;
        }

        void OnMessageReceivedFromServer(Base message)
        {
            if ((int)message.code % 10 == 0) // Success codes always have their unit number equal to 0 (cf. protocol)
            {
                Debug.Log(message.content);
                switch (message.code)
                {
                    case Code.register_viewer_success:
                        SceneManager.LoadSceneAsync(SceneNames.Game);
                        break;
                }
            }
            else
            {
                Debug.LogError(message.content);
                switch (message.code)
                {
                    case Code.join_game_error:
                        InstantiateErrorOverlay(StringLitterals.ERROR_WRONG_PIN);
                        break;
                }
            }
        }

        #endregion
    }

}
