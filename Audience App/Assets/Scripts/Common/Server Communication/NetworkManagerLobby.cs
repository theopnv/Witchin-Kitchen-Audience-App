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

        #endregion

        #region Emit

        public void EmitJoinGame(int roomPin)
        {
            var serialized = JsonConvert.SerializeObject(roomPin);
            _Socket.Emit(messages.Command.JOIN_GAME, new JSONObject(serialized));
        }

        #endregion


        #region Receive

        private void OnLobbyMessage(Base content)
        {
            if ((int)content.code % 10 == 0) // Success codes always have their unit number equal to 0 (cf. protocol)
            {
                Debug.Log(content.content);
                switch (content.code)
                {
                    case Code.join_game_success:
                        SceneManager.LoadSceneAsync(SceneNames.Game);
                        break;
                    default: break;
                }
            }
            else
            {
                Debug.LogError(content.content);
            }
        }

        #endregion

    }
}

