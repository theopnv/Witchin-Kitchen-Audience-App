using System.Collections;
using System.Collections.Generic;
using audience;
using audience.messages;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace audience.game
{

    public class GameOutcomePanelManager : APanelManager
    {
        [SerializeField] private Text _WinnerText;

        public GameOutcome GameOutcome;

        #region Custom Methods

        void Start()
        {
            _WinnerText.text = GameOutcome.winner.name + " won the game!";
        }

        #endregion
    }

}
