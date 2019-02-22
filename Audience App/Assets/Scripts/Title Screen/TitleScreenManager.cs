using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SocketIO;

public class TitleScreenManager : MonoBehaviour
{
    private const string _LOBBY_GAME_SCENE = "Lobby";

    void Start()
    {
        QualitySettings.vSyncCount = 1;
    }

    public void OnMainButtonClick()
    {
        SceneManager.LoadSceneAsync(_LOBBY_GAME_SCENE);
    }
}
