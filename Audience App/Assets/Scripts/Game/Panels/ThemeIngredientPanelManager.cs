using audience.game;
using audience.messages;
using UnityEngine;
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
        [SerializeField] private Button _VoteIngredientBButton;

        public void StartPoll()
        {
            SetButton(_VoteIngredientAButton, _IngredientPoll.ingredients[0]);
            SetButton(_VoteIngredientBButton, _IngredientPoll.ingredients[1]);
        }

        public void OnIngredientAClick()
        {
            _NetworkManager.SendIngredientVote(_VoteIngredientAButton.GetComponent<PollButton>().ID);
        }

        public void OnIngredientBClick()
        {
            _NetworkManager.SendIngredientVote(_VoteIngredientBButton.GetComponent<PollButton>().ID);
        }

        private void SetButton(Button button, Ingredient ig)
        {
            var eventButton = button.GetComponent<PollButton>();
            eventButton.ID = ig.id;
            button.GetComponentInChildren<Text>().text =
                Ingredients.IngredientNames[(Ingredients.IngredientID)ig.id];
            var sprite = Resources.Load<Sprite>("Spell/" +
               Ingredients.IngredientNames[(Ingredients.IngredientID)ig.id]);
            button.GetComponentInChildren<Image>().sprite = sprite;
        }

        #endregion

        private IngredientPoll _IngredientPoll;

        void Start()
        {
            _NetworkManager = FindObjectOfType<NetworkManager>();
            _NetworkManager.OnMessageReceived += OnMessageReceivedFromServer;
            
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
                        //SwitchSides();

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

        // TODO: Live results for ingredient vote

        #endregion

    }
}