using System.Collections;
using System.Collections.Generic;
using audience;
using audience.messages;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private NetworkManager _NetworkManager;

    [SerializeField] private GameObject _PollPanelPrefab;

    [HideInInspector]
    public PollPanelManager PollPanelManager;

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

    public void StartPoll(PollChoices pollChoices)
    {
        var pollPanel = Instantiate(_PollPanelPrefab);
        PollPanelManager = pollPanel.GetComponent<PollPanelManager>();
        PollPanelManager.StartPoll(pollChoices, _NetworkManager);
    }

}
