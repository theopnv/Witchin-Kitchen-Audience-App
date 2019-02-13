using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

namespace audience.lobby
{

    public class LobbyGames
    {
        public List<string> Games;
    }

    public class LobbyManager : MonoBehaviour
    {
        private const string _GAME_SCENE = "Game";

        private SocketIOComponent _Socket;

        void Start()
        {
            _Socket = GetComponent<SocketIOComponent>();

            _Socket.On("lobby", OnRoomListUpdated);

            StartCoroutine("SendRefreshRequest");
        }

        private IEnumerator SendRefreshRequest()
        {
            yield return new WaitForSeconds(1);
            _Socket.Emit("refreshLobby");
        }

        private void OnRoomListUpdated(SocketIOEvent e)
        {
            Debug.Log("[SocketIO] received a message: " + e.name);
            var roomList = JsonConvert.DeserializeObject<LobbyGames>(e.data.ToString());
            foreach (var room in roomList.Games)
            {
                Debug.Log(room);
            }
        }

        public void OnJoinButtonClick()
        {
            SceneManager.LoadSceneAsync(_GAME_SCENE);
        }
    }

}
