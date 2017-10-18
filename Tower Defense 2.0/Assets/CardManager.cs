using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour {

    [SerializeField] CardHolder cardHolder;

    Cards[] cards;
    Money money;

	// Use this for initialization
	void Start () {
        cards = GetComponentsInChildren<Cards>();
        money = FindObjectOfType<Money>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CardSelected(int cardSelected)
    {
       
        SetNewCards();
        GetMoney(cardSelected);

        gameObject.SetActive(false);
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
