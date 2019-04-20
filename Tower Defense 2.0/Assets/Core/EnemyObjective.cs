using UnityEngine;
using Towers.Enemies;

namespace Towers.Core
{
    public class EnemyObjective : MonoBehaviour
    {
        [SerializeField] ParticleSystem particle;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.GetComponent<EnemyAI>())
            {
                FindObjectOfType<LifePoints>().DamageLifePoints(collider.GetComponent<EnemyAI>().GetDamageToLifePoints());
                particle.transform.position = collider.transform.position;
                particle.Play();
                Destroy(collider.gameObject);
            }
        }
    }
}