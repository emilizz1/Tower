using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRunPlus : MonoBehaviour
{
    bool shouldCardDraft = false;

    public void SetCardDraft(bool value)
    {
        shouldCardDraft = value;
    }

    public bool GetCardDraft()
    {
        return shouldCardDraft;
    }
}
