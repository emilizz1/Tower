using UnityEngine;

namespace Towers.Enemies
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