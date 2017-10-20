using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour {

    [SerializeField] CardHolder cardHolder;

    Cards[] cards;
    Money money;
    Toggle toggle;
    GameObject prefab;
    bool objectSelected = false;
    
    // Use this for initialization
    void Start () {
        cards = GetComponentsInChildren<Cards>();
        money = FindObjectOfType<Money>();
        toggle = FindObjectOfType<Toggle>();
	}

    public GameObject GetPrefab()
    {
        return prefab;
    }

    public void CardSelected(int cardSelected)
    {
        objectSelected = true;
        prefab = cards[cardSelected].GetPrefab();
        GetMoney(cardSelected);
        toggle.isOn = false;
        SetNewCards();
        gameObject.SetActive(false);
    }

    public bool GetObjectSelected()
    {
        return objectSelected;
    }

    public void ChangeObjectSelected()
    {
        objectSelected = false;
    }


    void SetNewCards()
    {
        for (int i = 0; i <= 2; i++)
        {
            cards[i].SetCard(cardHolder.GetRandomCard());
        }
    }

    void GetMoney(int cardSelected)
    {
        for(int i=0;i<=2;i++)
        {
            if(cardSelected == i)
            {
                money.ChangeMoneyAmount(-cards[i].GetCardCost());
            }
            else
            {
                money.ChangeMoneyAmount(cards[i].GetCardCost());
            }
        }
    }
}
