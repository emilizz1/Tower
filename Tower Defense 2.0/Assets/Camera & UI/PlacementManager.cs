using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] GameObject parent;

    CardManager cardmanager;
    Rect screenRectOnConstruction = new Rect(0, 0, Screen.width, Screen.height);
    float maxRaycasterDepth = 100f;
    Friendly child = null;
    RaycastHit hitInfo;
    EnemySpawner enemySpawner;
    

    private void Start()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        cardmanager = FindObjectOfType<CardManager>();
    }

    void Update()
    {
        screenRectOnConstruction = new Rect(0, 0, Screen.width, Screen.height);
        PerformRaycast();
        if (child = GetComponentInChildren<Friendly>())
        {
            MoveObject();
        }
    }

    private void MoveObject()
    {

        child.transform.position = new Vector3(Mathf.Round(hitInfo.point.x), 0f, Mathf.Round(hitInfo.point.z));
        if (Input.GetMouseButtonDown(0))
        {
            child.transform.parent = parent.transform;
            enemySpawner.SpawnEnemies();
        }
        else if (Input.GetKeyDown("q"))
        {
            child.transform.Rotate(0f, -90f, 0f);
        }
        else if (Input.GetKeyDown("e"))
        {
            child.transform.Rotate(0f, 90f, 0f);
        }
    }

    void PerformRaycast()
    {
        
        if (screenRectOnConstruction.Contains(Input.mousePosition))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hitInfo, maxRaycasterDepth);
            if (cardmanager.GetObjectSelected())
            {
                Instantiate(cardmanager.GetPrefab(), hitInfo.point, Quaternion.identity, transform);
                cardmanager.ChangeObjectSelected();
            }
        }
    }
}
