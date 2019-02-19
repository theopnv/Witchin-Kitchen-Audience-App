using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasManager : MonoBehaviour
{
    public enum PanelId
    {
        primary_panel,
        poll_panel,
        game_over_panel,
    }

    [SerializeField] private GameObject _PrimaryPanel;
    [SerializeField] private GameObject _PollPanel;
    [SerializeField] private GameObject _GameOverPanel;

    private Dictionary<PanelId, GameObject> _Panels;

    private void Start()
    {
        _Panels = new Dictionary<PanelId, GameObject>
        {
            { PanelId.primary_panel, _PrimaryPanel },
            { PanelId.poll_panel, _PollPanel },
            { PanelId.game_over_panel, _GameOverPanel },
        };
    }

    public GameObject SetActivePanel(PanelId id)
    {
        foreach (var panel in _Panels)
        {
            panel.Value.SetActive(false);
        }

        _Panels[id].SetActive(true);

        return _Panels[id];
    }

}
