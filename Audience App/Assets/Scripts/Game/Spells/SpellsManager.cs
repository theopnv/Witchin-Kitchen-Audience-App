using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellsManager : MonoBehaviour
{
    public ScrollRect ScrollView;
    public GameObject ScrollContent;
    public GameObject CardPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GenerateCard();
        }

        ScrollView.horizontalNormalizedPosition = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateCard()
    {
        var cardInstance = Instantiate(CardPrefab);
        cardInstance.transform.SetParent(ScrollContent.transform, false);
    }
}
