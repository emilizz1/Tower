using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    [SerializeField] float buildUnitCost;
    [SerializeField] GameObject unitPrefab;
    [SerializeField] Transform placement;

    SelectedCanvas selectedCanvas;

    void Awake ()
    {
        selectedCanvas = FindObjectOfType<SelectedCanvas>();
    }

    public void Selected()
    {
        selectedCanvas.PutSelectedOnBuildButton(buildUnitCost, unitPrefab, placement);
    }

    public void Unselected()
    {
        selectedCanvas.ReturnToDefault();
    }
}
