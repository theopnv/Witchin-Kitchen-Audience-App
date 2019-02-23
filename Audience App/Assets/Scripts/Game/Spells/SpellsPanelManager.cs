using System;
using System.Collections;
using System.Collections.Generic;
using audience;
using audience.game;
using UnityEngine;
using UnityEngine.UI;

public class SpellsPanelManager : MonoBehaviour
{
    [SerializeField]
    private ScrollRect _ScrollView;

    [SerializeField]
    private GameObject _ScrollContent;

    [SerializeField]
    private GameObject _CardPrefab;

    private NetworkManager _NetworkManager;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Timer");
    }

    #region Custom Methods

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }

    public void GenerateSpellCards(NetworkManager networkManager)
    {
        _NetworkManager = networkManager;
        foreach (var spell in Spells.EventList)
        {
            GenerateCard(spell.Value);
        }
    }

    void GenerateCard(Type spellType)
    {
        var cardInstance = Instantiate(_CardPrefab);
        cardInstance.transform.SetParent(_ScrollContent.transform, false);
        cardInstance.AddComponent(spellType);

        var spellManager = cardInstance.GetComponent<ISpell>();
        spellManager.SetNetworkManager(_NetworkManager);

        var spellCardManager = cardInstance.GetComponent<SpellCardManager>();
        spellCardManager.Title.text = spellManager.GetTitle();
        //spellCardManager.Description.text = spellManager.GetDescription();
        spellCardManager.CastSpellButton.onClick.AddListener(spellManager.OnCastButtonClick);
    }

    #endregion
}
