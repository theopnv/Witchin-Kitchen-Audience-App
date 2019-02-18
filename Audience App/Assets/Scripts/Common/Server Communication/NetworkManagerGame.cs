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

        [SerializeField] private GameManager _GameManager;

        #region Unity API

        private void GameStart()
        {
            _Socket.On(Command.EVENT_LIST, OnEventList);
        }

        #endregion

        #region Emit

        public void SendVote(int eventId)
        {
            var serialized = JsonConvert.SerializeObject(eventId);
            _Socket.Emit(Command.SEND_VOTE, new JSONObject(serialized));
        }

        #endregion

        #region Receive

        private void OnGameMessage(Base content)
        {
            if ((int)content.code % 10 == 0) // Success codes always have their unit number equal to 0 (cf. protocol)
            {
                Debug.Log(content.content);
                switch (content.code)
                {
                    case Code.success_vote_accepted:
                        _GameManager?.PollPanelManager?.ClosePollPanel();
                        break;
                     break;
                }
            }
            else
            {
                Debug.LogError(content.content);
                switch (content.code)
                {
                    case Code.error_vote_didnt_pass:
                        break;
                    default: break;
                }
            }
        }

        private void OnEventList(SocketIOEvent e)
        {
            Debug.Log("OnEventList");
            _GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

            var pollChoices = JsonConvert.DeserializeObject<PollChoices>(e.data.ToString());
            _GameManager?.StartPoll(pollChoices);
        }

        #endregion

    }
}

