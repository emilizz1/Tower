using Towers.Enemies;
using UnityEngine;

namespace Towers.Units
{
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
}