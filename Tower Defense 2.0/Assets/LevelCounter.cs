using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCounter : MonoBehaviour
{
    int currentScene;

    public void LevelFinished( int level)
    {
        currentScene = level;
    }

    public int GetLevelFinished()
    {
        return currentScene;
    }
}
