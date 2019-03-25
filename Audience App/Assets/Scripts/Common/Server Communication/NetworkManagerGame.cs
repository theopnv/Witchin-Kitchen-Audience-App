using System;
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

        public Action<PollChoices> OnReceivedPollResults;
        public Action<PollChoices> OnReceivedPollList;
        public Action<SpellRequest> OnReceivedSpellRequest;
        public Action<GameOutcome> OnReceivedGameOutcome;
        public Action<Game> OnReceivedGameStateUpdate;
        public Action<EndGame> OnReceivedEndGame;
        public Action<IngredientPoll> OnReceivedIngredientPoll;
        public Action<IngredientPoll> OnReceivedIngredientPollResults;
        public Action OnReceivedStopIngredientPoll;

        #region Unity API

        private void GameStart()
        {
            _Socket.On(Command.EVENT_LIST, OnEventList);
            _Socket.On(Command.CAST_SPELL_REQUEST, OnCastSpellRequest);
            _Socket.On(Command.GAME_OUTCOME, OnGameOutcome);
            _Socket.On(Command.POLL_RESULTS, OnPollResults);
            _Socket.On(Command.END_GAME, OnEndGame);
            _Socket.On(Command.VOTE_FOR_EVENT, OnVoteForEvent);
            _Socket.On(Command.INGREDIENT_RESULTS, OnIngredientResults);
            _Socket.On(Command.STOP_INGREDIENT_POLL, OnStopIngredientPoll);
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
            _Socket.Emit(Command.CAST_SPELL_RESPONSE, new JSONObject(serialized));
        }

        public void SendIngredientVote(int ingredientId)
        {
            var serialized = JsonConvert.SerializeObject(ingredientId);
            _Socket.Emit(Command.VOTE_FOR_EVENT, new JSONObject(serialized));
        }

        #endregion

        #region Receive
        
        private void OnEventList(SocketIOEvent e)
        {
            Debug.Log("OnEventList");
            var pollChoices = JsonConvert.DeserializeObject<PollChoices>(e.data.ToString());
            OnReceivedPollList?.Invoke(pollChoices);
        }

        private void OnCastSpellRequest(SocketIOEvent e)
        {
            Debug.Log("OnCastSpellRequest");
            var spellRequest = JsonConvert.DeserializeObject<SpellRequest>(e.data.ToString());
            OnReceivedSpellRequest?.Invoke(spellRequest);
        }

        private void OnPollResults(SocketIOEvent e)
        {
            Debug.Log("OnPollResults");
            var pollChoices = JsonConvert.DeserializeObject<PollChoices>(e.data.ToString());
            OnReceivedPollResults?.Invoke(pollChoices);
        }

        private void OnGameOutcome(SocketIOEvent e)
        {
            Debug.Log("OnPlayerQOnGameOutcomeuitGame");
            var gameOutcome = JsonConvert.DeserializeObject<GameOutcome>(e.data.ToString());
            OnReceivedGameOutcome?.Invoke(gameOutcome);
        }

        private void OnEndGame(SocketIOEvent e)
        {
            Debug.Log("OnEndGame");
            var rematch = JsonConvert.DeserializeObject<EndGame>(e.data.ToString());
            OnReceivedEndGame?.Invoke(rematch);
        }

        private void OnVoteForEvent(SocketIOEvent e)
        {
            Debug.Log("OnVoteForEvent");
            var poll = JsonConvert.DeserializeObject<IngredientPoll>(e.data.ToString());
            OnReceivedIngredientPoll?.Invoke(poll);
        }

        private void OnIngredientResults(SocketIOEvent e)
        {
            Debug.Log("OnIngredientResults");
            var ingredientResults = JsonConvert.DeserializeObject<IngredientPoll>(e.data.ToString());
            OnReceivedIngredientPollResults?.Invoke(ingredientResults);
        }

        private void OnStopIngredientPoll(SocketIOEvent e)
        {
            Debug.Log("OnStopIngredientPoll");
            OnReceivedStopIngredientPoll?.Invoke();
        }

        #endregion
    }
}

