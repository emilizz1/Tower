using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHolder : MonoBehaviour
{
    [SerializeField] int startingGold = 3;
    [SerializeField] int startingWood = 2;
    [SerializeField] int startingCoal = 2;

    int currentGold;
    int currentWood;
    int currentCoal;

    void Awake()
    {
        currentGold = startingGold;
        currentWood = startingWood;
        currentCoal = startingCoal;
    }

    public int getCurrentGold()
    {
        return currentGold;
    }

    public int getCurrentWood()
    {
        return currentWood;
    }

    public int getCurrentCoal()
    {
        return currentCoal;
    }

    public void AddGold(int Amount)
    {
        currentGold += Amount;
    }

    public void AddWood(int Amount)
    {
        currentWood += Amount;
    }

    public void AddCoal(int Amount)
    {
        currentCoal += Amount;
    }
}
