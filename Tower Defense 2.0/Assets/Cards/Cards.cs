using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour {

    [SerializeField] Card card;
    
    RawImage rawImage;
    
    void Start ()
    {
        rawImage = GetComponentInChildren<RawImage>();
	}

    public GameObject GetPrefab()
    {
        return card.GetPrefab();
    }
	
    public float GetCardCost()
    {
        return card.GetCost();
    }

    public GameObject GetEnemyPrefab()
    {
        return card.GetEnemyPrefab();
    }

    public float GetEnemyAmount()
    {
        return card.GetEnemyAmount();
    }

    public void SetCard(Card cardToSet)
    {
        card = cardToSet;
        rawImage.texture = card.GetTexture();
    }
}
