using System;
using System.Collections;
using System.Collections.Generic;
using audience.messages;
using SocketIO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
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
        private Button[] _ColorButtons;

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
        }

        #endregion

        #region Custom Methods

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

        // TODO: Refactor this (dynamically add buttons and their listeners)
        // once the UIs are established.
        public void OnRedClick() { ViewerInfo.Color = Color.red; }
        public void OnBlueClick() { ViewerInfo.Color = Color.blue; }
        public void OnGreenClick() { ViewerInfo.Color = Color.green; }
        public void OnYellowClick() { ViewerInfo.Color = Color.yellow; }

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
                    default: break;
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
