using UnityEngine;
using Towers.Enemies;

namespace Towers.Core
{
    public class EnemyObjective : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.GetComponent<EnemyAI>())
            {
                FindObjectOfType<LifePoints>().DamageLifePoints(collider.GetComponent<EnemyAI>().GetDamageToLifePoints());
                Destroy(collider.gameObject);
            }
        }
    }
}