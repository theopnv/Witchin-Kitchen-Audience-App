using System.Linq;
using audience.game;
using audience.messages;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace audience.tutorial
{
    public class ThemeIngredientPanelManager : ATutorialPanelManager
    {

        [SerializeField] private GameObject _VotePanel;
        [SerializeField] private GameObject _ResultsPanel;

        private NetworkManager _NetworkManager;

        #region Vote panel

        [SerializeField] private Button _VoteIngredientAButton;
        [SerializeField] private Image _VoteIngredientAImage;
        [SerializeField] private Button _VoteIngredientBButton;
        [SerializeField] private Image _VoteIngredientBImage;

        public void StartPoll()
        {
            SetButton(_VoteIngredientAButton, _VoteIngredientAImage, IngredientPoll.ingredients[0]);
            SetButton(_VoteIngredientBButton, _VoteIngredientBImage, IngredientPoll.ingredients[1]);
        }

        public void OnIngredientAClick()
        {
            _NetworkManager.SendIngredientVote(_VoteIngredientAButton.GetComponent<PollButton>().ID);
        }

        public void OnIngredientBClick()
        {
            _NetworkManager.SendIngredientVote(_VoteIngredientBButton.GetComponent<PollButton>().ID);
        }

        private void SetButton(Button button, Image img, Ingredient ig)
        {
            var eventButton = button.GetComponent<PollButton>();
            eventButton.ID = ig.id;
            button.GetComponentInChildren<Text>().text =
                Ingredients.IngredientNames[(Ingredients.IngredientID)ig.id];
            var sprite = Resources.Load<Sprite>("Ingredients/" +
               Ingredients.IngredientNames[(Ingredients.IngredientID)ig.id]);
            img.sprite = sprite;
        }

        #endregion

        public IngredientPoll IngredientPoll;
        [SerializeField] private GameObject _PrimaryPanel;

        void Start()
        {
            _NetworkManager = FindObjectOfType<NetworkManager>();
            _NetworkManager.OnMessageReceived += OnMessageReceivedFromServer;
            _NetworkManager.OnReceivedIngredientPollResults += OnReceivedIngredientPollResults;
            _NetworkManager.OnReceivedStopIngredientPoll += OnReceivedStopIngredientPoll;
            StartPoll();
        }

        void OnDisable()
        {
            _NetworkManager.OnMessageReceived -= OnMessageReceivedFromServer;
        }

        public void SwitchSides()
        {
            _VotePanel.SetActive(false);
            _ResultsPanel.SetActive(true);
        }

        private void OnMessageReceivedFromServer(Base content)
        {
            if ((int)content.code % 10 == 0) // Success codes always have their unit number equal to 0 (cf. protocol)
            {
                switch (content.code)
                {
                    case Code.ingredient_vote_accepted:
                        SwitchSides();
                        break;
                }
            }
        }

        #region Results Panel

        [SerializeField] private Image _ResultIngredientAImage;
        [SerializeField] private Text _ResultsIngredientAText;
        [SerializeField] private Image _ResultIngredientBImage;
        [SerializeField] private Text _ResultsIngredientBText;
        [SerializeField] private Text _ResultsText;

        private void OnReceivedIngredientPollResults(IngredientPoll poll)
        {
            DisplayResults(poll.ingredients[0], _ResultIngredientAImage, _ResultsIngredientAText);
            DisplayResults(poll.ingredients[1], _ResultIngredientBImage, _ResultsIngredientBText);

            var mostVoted = poll.ingredients.OrderByDescending(i => i.votes).First();
            var name = Ingredients.IngredientNames[(Ingredients.IngredientID)mostVoted.id];
            _ResultsText.text = name + " is likely to be chosen as the theme ingredient!";
        }

        private void DisplayResults(Ingredient ig, Image img, Text text)
        {
            var sprite = Resources.Load<Sprite>("Ingredients/" +
                Ingredients.IngredientNames[(Ingredients.IngredientID)ig.id]);
            img.sprite = sprite;
            text.text = ig.votes + " votes";
        }

        private void OnReceivedStopIngredientPoll()
        {
            Destroy(gameObject);
            if (SceneManager.GetActiveScene().name == SceneNames.Game)
            {
                Instantiate(_PrimaryPanel, _Canvas.transform);
            }
        }

        #endregion

    }
}