﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] EnemyHolder enemyHolder;

    LifePoints lifepoints;
    EnemyRoundHolder[] enemies;
    Text text;
    int currentLevel = 1;

    void Start()
    {
        text = GetComponent<Text>();
        lifepoints = FindObjectOfType<LifePoints>();
    }

    void Update()
    {
        text.text = currentLevel.ToString() + " / " + enemyHolder.GetLevelCount().ToString();
        CheckForLevelLost();
    }

    public EnemyAI[] GetCurrentLevelEnemies()
    {
        return enemyHolder.GetEnemies(currentLevel - 1).GetEnemy();
    }

    void CheckForLevelLost()
    {
        if (lifepoints.GetLifePoints() == 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    public bool CheckForLevelWon()
    {
        if (currentLevel == enemyHolder.GetLevelCount() + 1)
        {
            return true;
        }
        return false;
    }

    public void LevelFinished()
    {
        currentLevel++;
    }
}
