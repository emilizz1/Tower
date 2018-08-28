using System.Collections;
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

	void Start () {
        text = GetComponent<Text>();
        lifepoints = FindObjectOfType<LifePoints>();
	}
	
	void Update () {
        text.text = currentLevel.ToString() + " / " + enemyHolder.GetLevelCount().ToString();
        CheckForLevelEnd();
	}

    public EnemyAI[] GetCurrentLevelEnemies()
    {
        return enemyHolder.GetEnemies(currentLevel-1).GetEnemy();
    }

    void CheckForLevelEnd()
    {
        if (currentLevel == enemyHolder.GetLevelCount() + 1)
        {
            SceneManager.LoadScene(2);
        }
        else if (lifepoints.GetLifePoints() == 0)
        {
            SceneManager.LoadScene(3);
        }
    }

    public void LevelFinished()
    {
        currentLevel++;
    }
}
