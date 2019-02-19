using System.Collections;
using System.Collections.Generic;
using audience;
using audience.messages;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private NetworkManager _NetworkManager;

    [SerializeField] private GameCanvasManager _CanvasManager;

    [HideInInspector] public PollPanelManager PollPanelManager;

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
        var pollPanel = _CanvasManager.SetActivePanel(GameCanvasManager.PanelId.poll_panel);
        PollPanelManager = pollPanel.GetComponent<PollPanelManager>();
        PollPanelManager.StartPoll(pollChoices, _NetworkManager);
    }

    public void SetGameOutcome(GameOutcome gameOutcome)
    {
        var exitRoompanel = _CanvasManager.SetActivePanel(GameCanvasManager.PanelId.game_over_panel);
        var exitRoomPanelManager = exitRoompanel.GetComponent<ExitRoomPanelManager>();
        exitRoomPanelManager.SetOutcome(gameOutcome);
    }

}
