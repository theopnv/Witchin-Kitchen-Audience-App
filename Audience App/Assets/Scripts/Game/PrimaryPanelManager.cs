using System.Collections;
using System.Collections.Generic;
using audience;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace audience.game
{
    public class PrimaryPanelManager : MonoBehaviour
    {
        public void ExitRoom()
        {
            SceneManager.LoadSceneAsync(SceneNames.TitleScreen);
        }
    }

}
