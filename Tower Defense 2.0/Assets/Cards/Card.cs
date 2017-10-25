using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = ("Tower defense/Card"))]
public class Card : ScriptableObject
{
    [SerializeField] float cost;
    [SerializeField] Texture image;
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float amount;

    public float GetCost()
    {
        return cost;
    }

    public Texture GetTexture()
    {
        return image;
    }

    public GameObject GetPrefab()
    {
        return prefab;
    }

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    public float GetEnemyAmount()
    {
        return amount;
    }
}