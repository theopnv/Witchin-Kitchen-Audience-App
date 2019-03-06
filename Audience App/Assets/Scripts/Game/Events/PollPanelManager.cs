using System;
using System.Collections;
using System.Collections.Generic;
using audience;
using audience.game;
using audience.messages;
using UnityEngine;
using UnityEngine.UI;
using Event = audience.messages.Event;

namespace audience.game
{

    public class PollPanelManager : APanelManager
    {
        [SerializeField]
        private Text _RemainingTimeText;

        [SerializeField] private Button _ButtonA;
        [SerializeField] private Button _ButtonB;

        private NetworkManager _NetworkManager;

        private PollChoices _PollChoices;
        private int _RemainingTime;

        public void StartPoll(PollChoices pollChoices, NetworkManager networkManager)
        {
;            _PollChoices = pollChoices;
            _NetworkManager = networkManager;

            var now = DateTime.Now.ToUniversalTime().ToString("u");
            _RemainingTime = pollChoices.duration;
            SetButton(_ButtonA, _PollChoices.events[0]);
            SetButton(_ButtonB, _PollChoices.events[1]);

            InvokeRepeating("UpdateTime", 0, 1);
        }

        private void SetButton(Button button, Event ev)
        {
            var eventButton = button.GetComponent<EventButton>();
            eventButton.EventID = ev.id;
            button.GetComponentInChildren<Text>().text = Events.EventList[(Events.EventID)ev.id];
        }

        private void UpdateTime()
        {
            if (_RemainingTime < 0)
            {
                ExitScreen();
            }
            _RemainingTimeText.text = "Remaining time to vote: " + _RemainingTime + " secs";
            --_RemainingTime;
        }

        public void OnEventAClick()
        {
            _NetworkManager.SendVote(_ButtonA.GetComponent<EventButton>().EventID);
        }

        public void OnEventBClick()
        {
            _NetworkManager.SendVote(_ButtonB.GetComponent<EventButton>().EventID);
        }

    }

}
