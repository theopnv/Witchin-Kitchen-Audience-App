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

        #region Unity API

        private void LobbyStart()
        {
            _Socket.On(Command.JOINED_GAME, OnJoinedGame);
        }

        #endregion

        #region Emit

        public void EmitJoinGame(int roomPin)
        {
            var serialized = JsonConvert.SerializeObject(roomPin);
            _Socket.Emit(messages.Command.JOIN_GAME, new JSONObject(serialized));
        }

        public void EmitViewerCharacteristics()
        {
            var viewer = new Viewer
            {
                name = ViewerInfo.Name,
                color = ColorUtility.ToHtmlStringRGBA(ViewerInfo.Color),
                socketId = ViewerInfo.SocketId,
            };
            var serialized = JsonConvert.SerializeObject(viewer);
            _Socket.Emit(Command.REGISTER_VIEWER, new JSONObject(serialized));
        }

        #endregion

        #region Receive
        
        private void OnJoinedGame(SocketIOEvent e)
        {
            var viewer = JsonConvert.DeserializeObject<Viewer>(e.data.ToString());
            Debug.Log("OnJoinedGame with socketId: " + viewer.socketId);
            ViewerInfo.SocketId = viewer.socketId;
            EmitViewerCharacteristics();
        }

        #endregion

    }
}

