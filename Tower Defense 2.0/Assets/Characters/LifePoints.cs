using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePoints : MonoBehaviour
{
    Text text;
    float lifePoints;

    void Start ()
    {
        text = GetComponent<Text>();
        lifePoints = 20;
	}
	
	void Update ()
    {
        text.text = lifePoints.ToString();
	}

    public void DamageLifePoints(float amount)
    {
        lifePoints -= amount;
    }

    public float GetLifePoints()
    {
        return lifePoints;
    }
}
