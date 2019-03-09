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
            _Socket.On(Command.UPDATED_GAME_STATE, OnGameStateUpdated);
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

        private void OnGameStateUpdated(SocketIOEvent e)
        {
            var game = JsonConvert.DeserializeObject<Game>(e.data.ToString());
            GameInfo.PlayerNumber = game.players.Count;
            for (var i = 0; i < game.players.Count; i++)
            {
                GameInfo.PlayerNames[i] = game.players[i].name;
                ColorUtility.TryParseHtmlString(game.players[i].color, out GameInfo.PlayerColors[i]);
                GameInfo.PlayerIDs[i] = i;
            }
        }

        #endregion

    }
}

