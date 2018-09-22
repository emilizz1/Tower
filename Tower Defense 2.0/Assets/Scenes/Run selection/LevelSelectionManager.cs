using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionManager : MonoBehaviour
{
    [SerializeField] LevelSelection lastCompletedLevel;
    [SerializeField] bool clean;

    Rect screenRectOnConstruction = new Rect(0, 0, Screen.width, Screen.height);
    float maxRaycasterDepth = 1000f;
    RaycastHit hitInfo;
    LoadLevel loadLevel;

    void Start()
    {
        loadLevel = GetComponent<LoadLevel>();
    }

    void Update ()
    {
        if (clean)
        {
            foreach (LevelSelection level in FindObjectsOfType<LevelSelection>())
            {
                level.IsActive(false);
            }
        }
        lastCompletedLevel.TurnOnAllnextLevels();
        //if (Input.GetMouseButtonDown(0))
        //{
        //    PerformRaycast();
        //    if (hitInfo.transform.gameObject.GetComponent<LevelSelection>())
        //    {
        //        loadLevel.LoadScene( hitInfo.transform.gameObject.GetComponent<LevelSelection>().GetLevel());
        //    }
        //}
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