using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour {

    [SerializeField] Card card;

    Text text;
    RawImage rawImage;

    // Use this for initialization
    void Start () {
        text = GetComponentInChildren<Text>();
        text.text = card.GetCost().ToString();
        rawImage = GetComponentInChildren<RawImage>();
        rawImage.texture = card.GetTexture();
	}

    public GameObject GetPrefab()
    {
        return card.GetPrefab();
    }
	
    public float GetCardCost()
    {
        return card.GetCost();
    }
    
    public void SetCard(Card cardToSet)
    {
        card = cardToSet;
        text.text = card.GetCost().ToString();
        rawImage.texture = card.GetTexture();
    }
}
