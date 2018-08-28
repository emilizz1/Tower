using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void Update()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyAI>() != null)
        {
            Destroy(gameObject);
        }
    }
}


