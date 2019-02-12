using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    private const string _GAME_SCENE = "Game";

    public void OnJoinButtonClick()
    {
        SceneManager.LoadSceneAsync(_GAME_SCENE);
    }
}
