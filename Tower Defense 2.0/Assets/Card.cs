using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("Tower defense/Card"))]
public class Card : ScriptableObject
{
    [SerializeField] float cost;

    public float GetCost()
    {
        return cost;
    }
}
