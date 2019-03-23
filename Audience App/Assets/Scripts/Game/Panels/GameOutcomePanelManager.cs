using audience.messages;
using UnityEngine;
using UnityEngine.UI;

namespace audience.game
{

    public class GameOutcomePanelManager : APanelManager
    {
        [SerializeField] private GameObject _Leaderboards;
        [SerializeField] private GameObject _PlayerScorePrefab;
        [SerializeField] private Text _RemainingTimeText;
        private int _RemainingTime = 15;

        public GameOutcome GameOutcome;

        #region Custom Methods

        void Start()
        {
            for (var i = 0; i < GameOutcome.leaderboards.Length; i++)
            {
                var playerScore = Instantiate(_PlayerScorePrefab, _Leaderboards.transform);
                var manager = playerScore.GetComponent<PlayerScore>();
                manager.GetComponent<Image>().color = GameInfo.PlayerColors[GameOutcome.leaderboards[i].id];

                manager.Name = GameOutcome.leaderboards[i].name;
                manager.Potions = GameOutcome.leaderboards[i].potions;
                manager.Ingredients = GameOutcome.leaderboards[i].ingredients;
            }

            _RemainingTimeText.text = "Remaining time before possible rematch: " + _RemainingTime + " seconds";
            InvokeRepeating("Timer", 1, 1);
        }

        void Timer()
        {
            --_RemainingTime;
            if (_RemainingTime <= 0)
            {
                _RemainingTime = 0;
            }
            _RemainingTimeText.text = "Remaining time before possible rematch: " + _RemainingTime + " seconds";
        }

        #endregion
    }

}
