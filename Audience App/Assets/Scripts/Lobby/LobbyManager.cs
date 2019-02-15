using System.Collections;
using System.Collections.Generic;
using audience.lobby.messages;
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
        [SerializeField]
        private InputField _RoomPinInputField;

        #region Private Attributes

        private const string _GAME_SCENE = "Game";

        private SocketIOComponent _Socket;

        #endregion

        #region Unity API

        void Start()
        {
            _Socket = GetComponent<SocketIOComponent>();

            //_Socket.On(messages.Command.ROOM_LIST, OnRoomListUpdated);
            _Socket.On(messages.Command.MESSAGE, OnMessage);

            StartCoroutine("SendRefreshRequest");
        }

        #endregion

        #region Custom Methods

        private void OnMessage(SocketIOEvent e)
        {
            var content = JsonConvert.DeserializeObject<Base>(e.data.ToString());
            if ((int)content.Code % 10 == 0) // Success codes always have their unit number equal to 0 (cf. protocol)
            {
                Debug.Log(content.Content);
                switch (content.Code)
                {
                    case Code.join_game_success:
                        SceneManager.LoadSceneAsync(_GAME_SCENE);
                        break;
                    default: break;
                }
            }
            else
            {
                Debug.LogError(content.Content);
            }
        } 

        private IEnumerator SendRefreshRequest()
        {
            yield return new WaitForSeconds(1);
            _Socket.Emit(messages.Command.REFRESH_LOBBY);
        }

        // Remove later on if considered useless
        //private void OnRoomListUpdated(SocketIOEvent e)
        //{
        //    Debug.Log("[SocketIO] received a message: " + e.name);
        //    var roomList = JsonConvert.DeserializeObject<LobbyGames>(e.data.ToString());
        //    foreach (var room in roomList.Games)
        //    {
        //        Debug.Log(room);
        //        var instance = Instantiate(_SelectRoomButtonPrefab, _ScrollView.transform);
        //        var button = instance.GetComponent<Button>();
        //        button.GetComponentInChildren<Text>().text = room;
        //    }
        //}

        public void OnJoinButtonClick()
        {
            if (!_RoomPinInputField.text.IsNullOrEmpty())
            {
                var asInt = int.Parse(_RoomPinInputField.text);
                var serialized = JsonConvert.SerializeObject(asInt);
                _Socket.Emit(messages.Command.JOIN_GAME, new JSONObject(serialized));
            }
            else
            {
                Debug.LogError("Please enter a room PIN");
            }
        }

        #endregion
    }

}
