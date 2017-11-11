using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjective : MonoBehaviour
{
    LifePoints lifePoints;

    private void Start()
    {
        lifePoints = FindObjectOfType<LifePoints>();
    }
    

    private void OnTriggerEnter(Collider collider)
    {
        
        if (collider.GetComponent<EnemyAI>())
        {
            lifePoints.DamageLifePoints(collider.GetComponent<EnemyAI>().GetDamageToLifePoints());
            DestroyObject(collider.gameObject);
        }
    }
}
