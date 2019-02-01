using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingTips : MonoBehaviour
{
    [SerializeField] Sprite[] loadingTips;
    
	void Start ()
    {
        GetComponent<Image>().sprite = loadingTips[Random.Range(0, loadingTips.Length)];
	}
}
