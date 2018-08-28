using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = ("Tower defense/Card"))]
public class Card : ScriptableObject
{
    [SerializeField] int cost;
    [SerializeField] Texture enemyImage;
    [SerializeField] BuildingsHolder prefabs;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float amount;

    public int GetEnemyGoldAmount()
    {
        return cost;
    }

    public Texture GetEnemyTexture()
    {
        return enemyImage;
    }

    public BuildingsHolder GetPrefabs()
    {
        return prefabs;
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