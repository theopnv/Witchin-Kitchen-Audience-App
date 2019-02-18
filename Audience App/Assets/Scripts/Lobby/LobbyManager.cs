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
        
        private NetworkManager _NetworkManager;

        #endregion

        void Start()
        {
            _NetworkManager = FindObjectOfType<NetworkManager>();
        }
        
        #region Custom Methods

        public void OnJoinButtonClick()
        {
            if (!_RoomPinInputField.text.IsNullOrEmpty())
            {
                var asInt = int.Parse(_RoomPinInputField.text);
                _NetworkManager.EmitJoinGame(asInt);
            }
            else
            {
                Debug.LogError("Please enter a room PIN");
            }
        }

        #endregion
    }

}
