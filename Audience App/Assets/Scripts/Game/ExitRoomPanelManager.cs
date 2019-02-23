using System.Collections;
using System.Collections.Generic;
using audience;
using audience.messages;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace audience.game
{

    public class ExitRoomPanelManager : MonoBehaviour
    {
        [SerializeField] private GameObject _GameFinished;
        [SerializeField] private GameObject _GameExited;

        [SerializeField] private Text _WinnerText;

        #region Custom Methods

        public void SetOutcome(GameOutcome gameOutcome)
        {
            _GameFinished.SetActive(gameOutcome.gameFinished);
            _GameExited.SetActive(!gameOutcome.gameFinished);

            if (gameOutcome.gameFinished)
            {
                _WinnerText.text = gameOutcome.winner.name + " won the game!";
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
