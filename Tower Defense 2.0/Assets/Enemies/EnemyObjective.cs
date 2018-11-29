using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjective : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        
        if (collider.GetComponent<EnemyAI>())
        {
            FindObjectOfType<LifePoints>().DamageLifePoints(collider.GetComponent<EnemyAI>().GetDamageToLifePoints());
            DestroyObject(collider.gameObject);
        }
    }
}
