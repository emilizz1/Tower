using Towers.Enemies;
using Towers.Units;
using UnityEngine;

namespace Towers.BuildingsN.Forest
{
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
            if (target != null)
            {
                Poison poison;

                if (target.GetComponent<Poison>())
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
    }
}