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
            var gameForViewer = JsonConvert.DeserializeObject<GameForViewer>(e.data.ToString());
            Debug.Log("OnJoinedGame with socketId: " + gameForViewer.viewer.socketId);
            ViewerInfo.SocketId = gameForViewer.viewer.socketId;
            GameInfo.PlayerNumber = gameForViewer.game.players.Count;
            for (var i = 0; i < gameForViewer.game.players.Count; i++)
            {
                GameInfo.PlayerNames[i] = gameForViewer.game.players[i].name;
                ColorUtility.TryParseHtmlString(gameForViewer.game.players[i].color, out GameInfo.PlayerColors[i]);
                GameInfo.PlayerIDs[i] = i;
            }
            EmitViewerCharacteristics();
        }

        #endregion

    }
}

