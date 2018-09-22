using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] int levelToLoad;
    [SerializeField] LevelSelection[] afterCompletionUnlocks;

	public int GetLevel()
    {
        return levelToLoad;
    }

    public void IsActive(bool isActive)
    {
        GetComponentInChildren<ParticleSystem>().gameObject.SetActive(isActive);
    }

    public void TurnOnAllnextLevels()
    {
        foreach(LevelSelection level in afterCompletionUnlocks)
        {
            level.IsActive(true);
        }
    }
}
