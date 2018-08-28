using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField] Text gold;
    [SerializeField] Text wood;
    [SerializeField] Text coal;
    [SerializeField] Sprite goldImage;
    [SerializeField] Sprite woodImage;
    [SerializeField] Sprite coalImage;

    int currentGold = 0;
    int currentWood = 1;
    int currentCoal = 1;

    void Start()
    {
        updateText();
    }

    void updateText()
    {
        gold.text = currentGold.ToString();
        wood.text = currentWood.ToString();
        coal.text = currentCoal.ToString();
    }

    public void AddResources(int amount = 0, Sprite image = null)
    {
        if (goldImage == image)
        {
            currentGold += amount;
        }
        else if (woodImage == image)
        {
            currentWood += amount;
        }
        else if (coalImage == image)
        {
            currentCoal += amount;
        }
        updateText();
    }

    public bool CheckForResources(int goldAmount = 0, int woodAmount = 0, int coalAmount = 0)
    {
        if (goldAmount <= currentGold && woodAmount <= currentWood && coalAmount <= currentCoal)
        {
            currentGold -= goldAmount;
            currentWood -= woodAmount;
            currentCoal -= coalAmount;
            updateText();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        updateText();
    }

    public Sprite GetResourceImages(bool gold = false, bool wood = false, bool coal = false)
    {
        if (gold)
        {
            return goldImage;
        }
        else if (wood)
        {
            return woodImage;
        }
        else if (coal)
        {
            return coalImage;
        }
        return null;
    }
}
	
