using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    private const string _LOBBY_GAME_SCENE = "Lobby";

    public void OnMainButtonClick()
    {
        SceneManager.LoadSceneAsync(_LOBBY_GAME_SCENE);
    }
}
