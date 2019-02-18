using System;
using System.Collections;
using System.Collections.Generic;
using audience.messages;
using Newtonsoft.Json;
using SocketIO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace audience
{

    public partial class NetworkManager : MonoBehaviour
    {

        private SocketIOComponent _Socket;

        /// <summary>
        /// Ensures that messages are contextualized.
        /// Only relevant messages are received in the current scene.
        /// </summary>
        private Dictionary<string, Delegate> _MessageFunctionMapper;

        [SerializeField] private Canvas _QuitGameCanvasPrefab;

        #region Unity API

        void Start()
        {
            _MessageFunctionMapper = new Dictionary<string, Delegate>()
            {
                { SceneNames.Lobby, (Action<Base>)OnLobbyMessage },
            };

            _Socket = GetComponent<SocketIOComponent>();
            
            _Socket.On(Command.MESSAGE, OnMessage);
            _Socket.On(Command.GAME_QUIT, OnPlayerQuitGame);
        }

        #endregion

        #region Emit

        public void ExitRoom()
        {
            _Socket.Emit(Command.DISCONNECT);
        }

        #endregion

        #region Receive

        private void OnMessage(SocketIOEvent e)
        {
            var content = JsonConvert.DeserializeObject<Base>(e.data.ToString());
            var currentSceneName = SceneManager.GetActiveScene().name;
            
            _MessageFunctionMapper[currentSceneName]?.DynamicInvoke(content);
        }

        private void OnPlayerQuitGame(SocketIOEvent e)
        {
            Debug.Log("OnPlayerQuitGame");
            var quitGameCanvasInstance = Instantiate(_QuitGameCanvasPrefab);
            quitGameCanvasInstance.gameObject.SetActive(true);
            Destroy(gameObject);
        }

        #endregion
    }
}

