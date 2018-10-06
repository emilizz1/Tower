using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyCanvas : MonoBehaviour
{
    void Awake()
    {
        int numDontDestroyCanvas = FindObjectsOfType<DontDestroyCanvas>().Length;
        if (numDontDestroyCanvas > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
