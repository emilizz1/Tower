using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayShowcaseSwitchButton : MonoBehaviour
{
    [SerializeField] GameObject showcase;
    [SerializeField] GameObject gameplay;

    bool isGameplayActive = true;

    public void Switch()
    {
        if (isGameplayActive)
        {
            gameplay.SetActive(false);
            showcase.SetActive(true);
        }
        else
        {
            gameplay.SetActive(true);
            showcase.SetActive(false);
        }
        isGameplayActive = !isGameplayActive;
    }
}
