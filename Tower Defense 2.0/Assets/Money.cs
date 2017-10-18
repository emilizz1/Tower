using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour {

    float money = 10;
    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        UpdateMoney();
	}

    public float GetMoney()
    {
        return money;
    }

    public void ChangeMoneyAmount(float amount)
    {
        money += amount;
        UpdateMoney();
    }
	
	void UpdateMoney()
    {
        text.text = money.ToString();
    }
}
