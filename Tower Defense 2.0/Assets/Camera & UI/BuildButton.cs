using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    [SerializeField] Transform parent;

    Text text;
    float csCost;
    GameObject csPrefab;
    Transform csPlacement;
    Money money;

	public void PutSelectedOn(float cost, GameObject prefab, Transform placement)
    {
        text = GetComponentInChildren<Text>();
        money = FindObjectOfType<Money>();

        gameObject.SetActive(true);
        text.text = cost.ToString();
        csCost = cost;
        csPlacement = placement;
        csPrefab = prefab;
    }

    public void TryToHire()
    {
        if (money.GetMoney() - csCost >= 0)
        {
            money.ChangeMoneyAmount(-csCost);
            Instantiate(csPrefab, csPlacement.position, Quaternion.identity, parent);
        }
    }
}
