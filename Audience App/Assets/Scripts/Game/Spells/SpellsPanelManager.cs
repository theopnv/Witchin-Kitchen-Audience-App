using System.Collections;
using System.Collections.Generic;
using audience.game;
using UnityEngine;
using UnityEngine.UI;

public class SpellsPanelManager : MonoBehaviour
{
    public ScrollRect ScrollView;
    public GameObject ScrollContent;
    public GameObject CardPrefab;

    // Start is called before the first frame update
    void Start()
    {
        ScrollView.horizontalNormalizedPosition = 1;
    }

    #region Custom Methods

    public void GenerateSpellCards()
    {
        foreach (var spell in Spells.EventList)
        {
            GenerateCard(spell.Value);
        }
    }

    void GenerateCard(string spell)
    {
        var cardInstance = Instantiate(CardPrefab);
        cardInstance.transform.SetParent(ScrollContent.transform, false);
    }

    #endregion
}
