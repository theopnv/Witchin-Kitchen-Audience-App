using System.Collections;
using System.Collections.Generic;
using audience;
using audience.messages;
using UnityEngine;
using UnityEngine.UI;

public class PollPanelManager : MonoBehaviour
{
    [SerializeField]
    private Text _RemainingTimeText;

    [SerializeField] private EventButton _ButtonA;
    [SerializeField] private EventButton _ButtonB;

    private NetworkManager _NetworkManager;

    private PollChoices _PollChoices;
    private float _RemainingTime;

    public void StartPoll(PollChoices pollChoices, NetworkManager networkManager)
    {
        _PollChoices = pollChoices;
        _RemainingTime = _PollChoices.pollingTime;
        _ButtonA.EventID = _PollChoices.events[0].id;
        _ButtonB.EventID = _PollChoices.events[1].id;
        InvokeRepeating("UpdateTime", 0, 1);
    }

    private void UpdateTime()
    {
        if (_RemainingTime < 0)
        {
            ClosePollPanel();
        }
        _RemainingTimeText.text = "Remaining time to vote: " + _RemainingTime + " secs";
        --_RemainingTime;
    }

    private void OnEventAClick()
    {
        _NetworkManager.SendVote(_ButtonA.EventID);
    }

    private void OnEventBClick()
    {
        _NetworkManager.SendVote(_ButtonB.EventID);
    }

    public void ClosePollPanel()
    {
        Destroy(gameObject);
    }
}
