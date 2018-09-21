using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionManager : MonoBehaviour
{
    Rect screenRectOnConstruction = new Rect(0, 0, Screen.width, Screen.height);
    float maxRaycasterDepth = 1000f;
    RaycastHit hitInfo;
    LoadLevel loadLevel;

    void Start()
    {
        loadLevel = GetComponent<LoadLevel>();
    }

    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            PerformRaycast();
            if (hitInfo.transform.gameObject.GetComponent<LevelSelection>())
            {
                loadLevel.LoadScene( hitInfo.transform.gameObject.GetComponent<LevelSelection>().GetLevel());
            }
        }
	}

    public void PerformRaycast()
    {
        if (screenRectOnConstruction.Contains(Input.mousePosition))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hitInfo, maxRaycasterDepth);
        }
    }
}
