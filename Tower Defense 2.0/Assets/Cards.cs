using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour {

    [SerializeField] Card card;

    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        text.text = card.GetCost().ToString();
	}
	
    public float GetCardCost()
    {
        return card.GetCost();
    }
    
    public void SetCard(Card cardToSet)
    {
        card = cardToSet;
        text.text = card.GetCost().ToString();
    }
}
