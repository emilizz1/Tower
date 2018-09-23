using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionManager : MonoBehaviour
{
    [SerializeField] LevelSelection lastCompletedLevel;

    Rect screenRectOnConstruction = new Rect(0, 0, Screen.width, Screen.height);
    float maxRaycasterDepth = 1000f;
    RaycastHit hitInfo;
    LoadLevel loadLevel;

    void Start()
    {
        loadLevel = GetComponent<LoadLevel>();
        ActivateActiveLevels();
    }

    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PerformRaycast();
            if (hitInfo.transform.gameObject.GetComponent<LevelSelection>() && hitInfo.transform.gameObject.GetComponent<LevelSelection>().isActive)
            {
                loadLevel.LoadScene(hitInfo.transform.gameObject.GetComponent<LevelSelection>().GetLevel());
            }
        }
    }

    void ActivateActiveLevels()
    {
        foreach (LevelSelection level in FindObjectsOfType<LevelSelection>())
        {
            level.IsActive(false);
        }
        lastCompletedLevel.TurnOnAllnextLevels();
    }

    void PerformRaycast()
    {
        if (screenRectOnConstruction.Contains(Input.mousePosition))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hitInfo, maxRaycasterDepth);
        }
    }
}