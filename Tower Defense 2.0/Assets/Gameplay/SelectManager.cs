using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    [SerializeField] ParticleSystem psBuildings;
    [SerializeField] ParticleSystem psUnit;

    CardManager cardManager;
    RaycastHit hitInfo;
    Buildings selectedBuilding;
    Character selectedCharacter;

    void Start()
    {
        cardManager = FindObjectOfType<CardManager>(); 
    }

    void checkForSelected()
    {
        if (hitInfo.transform.GetComponent<Buildings>() && (selectedBuilding != hitInfo.transform.GetComponent<Buildings>()))
        {
            UnselectBuilding();
            selectedBuilding = hitInfo.transform.GetComponent<Buildings>();
            Instantiate(psBuildings, selectedBuilding.transform.position, psBuildings.transform.rotation,selectedBuilding.transform);
            if (selectedCharacter!= null && selectedCharacter.GetComponentInChildren<ParticleSystem>())
            {
                Destroy(selectedCharacter.GetComponentInChildren<ParticleSystem>());
            }
            selectedCharacter = null;
        }
        else if (hitInfo.transform.GetComponent<FriendlyAI>() && (selectedCharacter != hitInfo.transform.GetComponent<Character>()))
        {
            selectedCharacter = hitInfo.transform.GetComponent<Character>();
            Instantiate(psUnit, selectedCharacter.transform.position, psUnit.transform.rotation, selectedCharacter.transform);
            UnselectBuilding();
        }
    }

    void UnselectBuilding()
    {
        if (selectedBuilding != null)
        {
            Destroy(selectedBuilding.GetComponentInChildren<ParticleSystem>());
            selectedBuilding = null;
        }
    }
}
