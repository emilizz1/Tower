using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifePoints : MonoBehaviour
{
    Text text;
    float lifePoints;

    public static LifePoints control;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != null)
        {
            Destroy(gameObject);
        }
    }

    void Start ()
    {
        text = GetComponent<Text>();
        lifePoints = 20;
	}
	
	void Update ()
    {
        text.text = lifePoints.ToString();
        if(lifePoints == 0)
        {
            SceneManager.LoadScene(2);
        }
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
