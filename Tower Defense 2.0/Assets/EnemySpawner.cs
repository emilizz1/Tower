using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject enemyHolder;
    [SerializeField] GameObject spawnPlace;

    CardManager cardManager;
    List<GameObject> enemies;

	// Use this for initialization
	void Start () {
        cardManager = FindObjectOfType<CardManager>();
	}

    public void SpawnEnemies()
    {
        enemies = new List<GameObject>(cardManager.GetEnemiesToCome());
        StartCoroutine(SpawningEnemies());
    }

    private IEnumerator SpawningEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            Instantiate(enemy, spawnPlace.transform.position, Quaternion.identity, enemyHolder.transform);
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}
