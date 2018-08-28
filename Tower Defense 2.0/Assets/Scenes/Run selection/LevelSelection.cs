using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour {

    [SerializeField] int levelToLoad;

	public int GetLevel()
    {
        return levelToLoad;
    }
}
