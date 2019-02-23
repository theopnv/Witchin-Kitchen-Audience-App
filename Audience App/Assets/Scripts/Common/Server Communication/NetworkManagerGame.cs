using System.Collections;
using System.Collections.Generic;
using audience.game;
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
            _Socket.On(Command.CAST_SPELL, OnCastSpellRequest);
        }

        #endregion

        #region Emit

        public void SendVote(int eventId)
        {
            var serialized = JsonConvert.SerializeObject(eventId);
            _Socket.Emit(Command.SEND_VOTE, new JSONObject(serialized));
        }

        public void EmitSpellCast(Spell spell)
        {
            var serialized = JsonConvert.SerializeObject(spell);
            _Socket.Emit(Command.CAST_SPELL, new JSONObject(serialized));
        }

        #endregion

        #region Receive

        private void OnGameMessage(Base content)
        {
            if ((int)content.code % 10 == 0) // Success codes always have their unit number equal to 0 (cf. protocol)
            {
                Debug.Log(content.code + ": " + content.content);
                switch (content.code)
                {
                    case Code.success_vote_accepted:
                        Destroy(_GameManager?.PollPanelManager.gameObject);
                        break;
                    case Code.spell_casted_success:
                        Destroy(_GameManager?.SpellsPanelManager.gameObject);
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
            RetrieveGameManager();

            var pollChoices = JsonConvert.DeserializeObject<PollChoices>(e.data.ToString());
            _GameManager?.StartPoll(pollChoices);
        }

        private void OnCastSpellRequest(SocketIOEvent e)
        {
            Debug.Log("OnCastSpellRequest");
            RetrieveGameManager();
            _GameManager?.StartSpellSelection();
        }

        private void RetrieveGameManager()
        {
            if (_GameManager == null)
            {
                _GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            }
        }

        #endregion

    }
}

