using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Discard : MonoBehaviour
{
    List<Card> discard = new List<Card>();
    Text text;

    void Start()
    {
        text = GetComponentInChildren<Text>();
        UpdateText();
    }

    public List<Card> GetDiscard()
    {
        List<Card> tempDiscard = new List<Card>(discard);
        discard.RemoveRange(0, discard.Count);
        return tempDiscard;
    }

    public void DiscardCards(List<Card> discardedCards)
    {
        discard.AddRange(discardedCards);
        UpdateText();
    }

    void UpdateText()
    {
        text.text = discard.Count.ToString();
    }
}
