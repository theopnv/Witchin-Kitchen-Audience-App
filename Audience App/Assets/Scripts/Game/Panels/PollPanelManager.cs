using audience.messages;
using UnityEngine;
using UnityEngine.UI;
using Event = audience.messages.Event;

namespace audience.game
{

    public class PollPanelManager : APanelManager
    {

        #region Vote Side

        [SerializeField] private GameObject _VoteSide;

        [SerializeField]
        private Text _RemainingTimeTextVote;

        [SerializeField] private Button _ButtonA;
        [SerializeField] private Image _ButtonAImage;
        [SerializeField] private Button _ButtonB;
        [SerializeField] private Image _ButtonBImage;

        private NetworkManager _NetworkManager;

        private int _RemainingTime;

        public void StartPoll()
        {
            _RemainingTime = PollChoices.duration;
            SetButton(_ButtonA, _ButtonAImage, PollChoices.events[0]);
            SetButton(_ButtonB, _ButtonBImage, PollChoices.events[1]);
            _RemainingTimeText = _RemainingTimeTextVote;
            Handheld.Vibrate();

            InvokeRepeating("UpdateTime", 0, 1);
        }

        private void SetButton(Button button, Image img, Event ev)
        {
            var sprite = Resources.Load<Sprite>("Events/" + Events.EventList[(Events.EventID) ev.id]);
            if (sprite)
            {
                img.sprite = sprite;
            }
            var eventButton = button.GetComponent<PollButton>();
            eventButton.ID = ev.id;
            button.GetComponentInChildren<Text>().text = Events.EventList[(Events.EventID)ev.id];
        }

        private void UpdateTime()
        {
            if (_RemainingTime < 0)
            {
                ExitScreen();
            }
            _RemainingTimeText.text = "Remaining time to vote: " + _RemainingTime + " seconds";
            --_RemainingTime;
        }

        public void OnEventAClick()
        {
            _NetworkManager.SendVote(_ButtonA.GetComponent<PollButton>().ID);
        }

        public void OnEventBClick()
        {
            _NetworkManager.SendVote(_ButtonB.GetComponent<PollButton>().ID);
        }

        #endregion

        #region Common

        public PollChoices PollChoices;

        [HideInInspector]
        public bool WasChoosingASpell;

        private bool IsVoting;
        private Text _RemainingTimeText;

        void Start()
        {
            _NetworkManager = FindObjectOfType<NetworkManager>();
            _NetworkManager.OnReceivedPollResults += OnReceivePollResults;
            _NetworkManager.OnMessageReceived += OnMessageReceivedFromServer;

            IsVoting = true;
            StartPoll();
        }

        void OnDisable()
        {
            _NetworkManager.OnReceivedPollResults -= OnReceivePollResults;
            _NetworkManager.OnMessageReceived -= OnMessageReceivedFromServer;
        }

        public void SwitchSides()
        {
            if (WasChoosingASpell)
            {
                IsVoting = false;
                ExitScreen();
            }
            else
            {
                _VoteSide.SetActive(false);
                _ResultsSide.SetActive(true);
                IsVoting = false;
                _RemainingTimeText = _RemainingTimeTextResults;
            }
        }

        public override void ExitScreen()
        {
            if (IsVoting)
            {
                SwitchSides();
            }
            else
            {
                base.ExitScreen();
            }
        }

        public void OnBackButtonClick()
        {
            ExitScreen();
        }

        private void OnMessageReceivedFromServer(Base content)
        {
            if ((int)content.code % 10 == 0) // Success codes always have their unit number equal to 0 (cf. protocol)
            {
                switch (content.code)
                {
                    case Code.success_vote_accepted:
                        SwitchSides();
                        break;
                }
            }
        }

        #endregion

        #region Results Side

        [SerializeField]
        private Text _RemainingTimeTextResults;

        [SerializeField] private GameObject _ResultsSide;

        [SerializeField] private Image _ImageA;
        [SerializeField] private Text _EventAResults;
        [SerializeField] private Image _ImageB;
        [SerializeField] private Text _EventBResults;

        public void OnReceivePollResults(PollChoices pollChoices)
        {
            PollChoices = pollChoices;
            DisplayEventResults(pollChoices.events[0], _ImageA, _EventAResults);
            DisplayEventResults(pollChoices.events[1], _ImageB, _EventBResults);
        }

        private void DisplayEventResults(Event ev, Image img, Text text)
        {
            var name = Events.EventList[(Events.EventID)ev.id];
            var msg = name + "\n" + ev.votes + " votes";
            text.text = msg;

            var sprite = Resources.Load<Sprite>("Events/" + Events.EventList[(Events.EventID)ev.id]);
            if (sprite)
            {
                img.sprite = sprite;
            }
        }

        #endregion


    }

}
