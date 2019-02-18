using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasManager : MonoBehaviour
{
    public enum PanelId
    {
        primary_panel,
        poll_panel,
    }

    [SerializeField] private GameObject _PrimaryPanel;
    [SerializeField] private GameObject _PollPanel;

    private Dictionary<PanelId, GameObject> _Panels;

    private void Start()
    {
        _Panels = new Dictionary<PanelId, GameObject>
        {
            { PanelId.primary_panel, _PrimaryPanel },
            { PanelId.poll_panel, _PollPanel },
        };
    }

    public GameObject SetActivePanel(PanelId id)
    {
        foreach (var panel in _Panels)
        {
            panel.Value.SetActive(false);
        }

        switch (id)
        {
            case PanelId.primary_panel:
                _PrimaryPanel.SetActive(true);
                return _PrimaryPanel;
            case PanelId.poll_panel:
                _PollPanel.SetActive(true);
                return _PollPanel;
            default: break;
        }

        return null;
    }

}
