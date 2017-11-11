using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour {

    [SerializeField] CardHolder cardHolder;
    
    Cards[] cards;
    List<GameObject> enemies = new List<GameObject>();
    Money money;
    Toggle toggle;
    GameObject prefab;
    bool objectSelected = false;
    
    // Use this for initialization
    void Start () {
        cards = GetComponentsInChildren<Cards>();
        money = FindObjectOfType<Money>();
        toggle = FindObjectOfType<Toggle>();
        toggle.isOn = true;
        SetNewCards();
    }

    public bool GetToggleState()
    {
        return toggle.isOn;
    }

    public GameObject GetPrefab()
    {
        return prefab;
    }

    public List<GameObject> GetEnemiesToCome()
    {
        return enemies;
    }

    public void CardSelected(int cardSelected)
    {
        objectSelected = true;
        prefab = cards[cardSelected].GetPrefab();
        GetMoney(cardSelected);
        SetEnemies(cardSelected);
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

    void SetEnemies(int cardSelected)
    {
        enemies = new List<GameObject>();
        for (int i = 0; i <= 2; i++)
        {
            if (cardSelected != i)
            {
                for (int o = 0; o < cards[i].GetEnemyAmount(); o++)
                {
                    enemies.Add(cards[i].GetEnemyPrefab());
                }
            }
        }
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
