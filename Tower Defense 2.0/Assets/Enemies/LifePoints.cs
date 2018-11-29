using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifePoints : MonoBehaviour
{
    [SerializeField] float startingLifepoints = 20;

    Text text;
    float lifePoints;

    void Start ()
    {
        text = GetComponent<Text>();
        lifePoints = startingLifepoints;
        UpdateLifePoints();

    }
	
	void UpdateLifePoints()
    {
        text.text = lifePoints.ToString();
	}

    public void DamageLifePoints(float amount)
    {
        lifePoints -= amount;
        if(lifePoints <= 0f)
        {
            SceneManager.LoadScene(2);
        }
        UpdateLifePoints();
    }

    public float GetLifePoints()
    {
        return lifePoints;
    }
}
