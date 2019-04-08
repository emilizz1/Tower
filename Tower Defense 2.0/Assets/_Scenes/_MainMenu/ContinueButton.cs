using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Towers.Scenes;
using Towers.Core;

public class ContinueButton : MonoBehaviour
{
    SaveLoad saveLoad;

    void Start()
    {
        saveLoad = FindObjectOfType<SaveLoad>();
        int levelToContinue = saveLoad.LoadIntInfo("Level");
        if(levelToContinue< 1 || levelToContinue > 5)
        {
            gameObject.SetActive(false);
        }
    }
    
    public void ContinuePlaying()
    {
        saveLoad.LoadPlayerResources();
        FindObjectOfType<LifePoints>().GiveNewLifepoints(saveLoad.LoadIntInfo("Lifepoints"));
        FindObjectOfType<LoadLevel>().LoadScene(3);
    }
}
