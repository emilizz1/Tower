using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePoints : MonoBehaviour
{
    [SerializeField] float startingLifepoints = 20;

    Text text;
    float lifePoints;

    void Start ()
    {
        text = GetComponent<Text>();
        lifePoints = startingLifepoints;
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
