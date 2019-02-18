using System.Collections;
using System.Collections.Generic;
using audience;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private NetworkManager _NetworkManager;

    // Start is called before the first frame update
    void Start()
    {
        _NetworkManager = FindObjectOfType<NetworkManager>();
    }

    public void ExitRoom()
    {
        _NetworkManager.ExitRoom();
        Destroy(_NetworkManager.gameObject);
        SceneManager.LoadSceneAsync(SceneNames.TitleScreen);
    }

}
