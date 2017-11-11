using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCanvas : MonoBehaviour
{
    [SerializeField] BuildButton buildButton;

    void Start()
    {
        ReturnToDefault();
    }

    public void PutSelectedOnBuildButton(float cost, GameObject prefab, Transform placement)
    {
        buildButton.gameObject.SetActive(true);
        buildButton.PutSelectedOn(cost, prefab, placement);
    }

    public void ReturnToDefault()
    {
        buildButton.gameObject.SetActive(false);
    }
}
