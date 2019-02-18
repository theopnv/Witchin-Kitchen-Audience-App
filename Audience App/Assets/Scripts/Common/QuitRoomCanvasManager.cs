using System.Collections;
using System.Collections.Generic;
using audience;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitRoomCanvasManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("GoBackToMenu");
    }

    #region Custom Methods

    private IEnumerator GoBackToMenu()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync(SceneNames.TitleScreen);
    }

    #endregion
}
