using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : MonoBehaviour
{
    [SerializeField] GameObject[] poisonPS;

    FriendlyAI friendlyAI;
    EnemyAI target;

    void Start() 
    {
        friendlyAI = GetComponent<FriendlyAI>();
    }

    public void Shoot()
    {
        target = friendlyAI.GetTarget();
        Poison poison;
        
        if (target != null && target.GetComponent<Poison>())
        {
            poison = target.GetComponent<Poison>();
            poison.AddPoison();
        }
        else
        {
            target.gameObject.AddComponent<Poison>();
        }
        Instantiate(poisonPS[target.GetComponent<Poison>().GetCurrentPoison() - 1], target.transform);
        target = null;
    }
}
