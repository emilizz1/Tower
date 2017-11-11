using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    PlacementManager raycast;
    CardManager cardManager;
    RaycastHit hitInfo;
    Buildings selectedBuilding;
    Character selectedCharacter;

    void Start()
    {
        raycast = GetComponent<PlacementManager>();
        cardManager = FindObjectOfType<CardManager>();
    }

    void Update()
    {
        if (!cardManager.GetToggleState() && Input.GetMouseButtonDown(0))
        {
            checkForSelected();
        }
        else if (!cardManager.GetToggleState() && selectedCharacter != null && Input.GetMouseButtonDown(1))
        {
            MoveSelectedCharacter();
        }
    }

    void MoveSelectedCharacter()
    {
        hitInfo = raycast.GetHitInfo();
        selectedCharacter.SetDestination(hitInfo.point);
    }

    void checkForSelected()
    {
        hitInfo = raycast.GetHitInfo();
        if (hitInfo.transform.GetComponent<Buildings>() && (selectedBuilding != hitInfo.transform.GetComponent<Buildings>()))
        {
            Unselect();
            selectedBuilding = hitInfo.transform.GetComponent<Buildings>();
            selectedBuilding.Selected();
        }
        else if (hitInfo.transform.GetComponent<FriendlyAI>() && (selectedCharacter != hitInfo.transform.GetComponent<Character>()))
        {
            selectedCharacter = hitInfo.transform.GetComponent<Character>();
            Unselect();
        }
    }

    void Unselect()
    {
        if (selectedBuilding != null)
        {
            selectedBuilding.Unselected();
        }
    }
}
