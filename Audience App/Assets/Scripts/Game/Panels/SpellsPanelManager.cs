using System;
using System.Collections;
using System.Collections.Generic;
using audience;
using audience.game;
using audience.messages;
using UnityEngine;
using UnityEngine.UI;

public class SpellsPanelManager : APanelManager
{
    [SerializeField]
    private ScrollRect _ScrollView;

    [SerializeField]
    private GameObject _ScrollContent;

    [SerializeField]
    private GameObject _CardPrefab;

    private NetworkManager _NetworkManager;
    public bool AuthorizeCasting;

    void Start()
    {
        _NetworkManager = FindObjectOfType<NetworkManager>();
        _NetworkManager.OnMessageReceived += OnMessageReceivedFromServer;
        foreach (var spell in Spells.EventList)
        {
            GenerateCard(spell.Value);
        }
        StartCoroutine("Timer");
    }

    #region Custom Methods

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(20);
        ExitScreen();
    }

    void GenerateCard(Type spellType)
    {
        var cardInstance = Instantiate(_CardPrefab);
        cardInstance.transform.SetParent(_ScrollContent.transform, false);
        cardInstance.AddComponent(spellType);

        var spellManager = cardInstance.GetComponent<ISpell>();
        spellManager.SetNetworkManager(_NetworkManager);

        var spellCardManager = cardInstance.GetComponent<SpellCardManager>();
        spellCardManager.AuthorizeCasting = AuthorizeCasting;
        spellCardManager.RectoTitle.text = spellManager.GetTitle();
        spellCardManager.RectoDescription.text = spellManager.GetDescription();
        spellCardManager.CastSpellAction += spellManager.OnCastButtonClick;
    }

    public void OnBackButtonClick()
    {
        ExitScreen();
    }

    void OnDisable()
    {
        _NetworkManager.OnMessageReceived -= OnMessageReceivedFromServer;
    }

    private void OnMessageReceivedFromServer(Base content)
    {
        if ((int)content.code % 10 == 0) // Success codes always have their unit number equal to 0 (cf. protocol)
        {
            Debug.Log(content.code + ": " + content.content);
            switch (content.code)
            {
                case Code.spell_casted_success:
                    ExitScreen();
                    break;
            }
        }
    }

    #endregion
}
