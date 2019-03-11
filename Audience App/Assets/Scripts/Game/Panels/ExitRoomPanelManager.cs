using System.Collections;
using System.Collections.Generic;
using audience;
using audience.messages;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace audience.game
{

    public class ExitRoomPanelManager : APanelManager
    {
        [SerializeField] private GameObject _GameFinished;
        [SerializeField] private GameObject _GameExited;

        [SerializeField] private Text _WinnerText;

        public GameOutcome GameOutcome;

        #region Custom Methods

        void Start()
        {
            _GameFinished.SetActive(GameOutcome.gameFinished);
            _GameExited.SetActive(!GameOutcome.gameFinished);

            if (GameOutcome.gameFinished)
            {
                _WinnerText.text = GameOutcome.winner.name + " won the game!";
                StartCoroutine("GoBackToMenu", 10);
            }
            else
            {
                StartCoroutine("GoBackToMenu", 2);
            }
        }

        private IEnumerator GoBackToMenu(int delay)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadSceneAsync(SceneNames.TitleScreen);
        }

        #endregion
    }

}
