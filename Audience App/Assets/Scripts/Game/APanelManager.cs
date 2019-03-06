using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace audience.game
{

    public abstract class APanelManager : MonoBehaviour
    {

        protected void ExitScreen()
        {
            var gameManager = GameObject.FindGameObjectWithTag("GameController");
            gameManager?.GetComponent<GameManager>()?.DestroyLastPanel();
        }

        public void OnBackButtonClick()
        {
            ExitScreen();
        }
    }

}
