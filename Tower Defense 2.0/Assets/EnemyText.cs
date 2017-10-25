using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyText : MonoBehaviour {

    Text text;
    Cards card;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        card = GetComponentInParent<Cards>();
    }

    // Update is called once per frame
    void Update ()
    {
        text.text = card.GetEnemyPrefab().name + " X " + card.GetEnemyAmount().ToString();
    }
}
