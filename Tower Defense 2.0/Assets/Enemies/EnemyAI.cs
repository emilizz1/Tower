using Towers.CharacterN;
using UnityEngine;
using UnityEngine.AI;

namespace Towers.Enemies
{
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(HealthSystem))]
    public class EnemyAI : MonoBehaviour
    {
        [Header("Nav Mesh Agent")]
        [SerializeField] float stoppingDistance = 1f;
        [SerializeField] float navMeshSteeringSpeed = 1f;
        [SerializeField] int damageToLifePoints = 1;
        [SerializeField] float navMeshRadius = 0.25f;

        NavMeshAgent navMeshAgent;
        WaypointContainer patrolPath;
        int nextWaypointIndex;
        Character character;

        void Awake()
        {
            AddNavMeshAgent();
        }

        void Start()
        {
            character = GetComponent<Character>();
            patrolPath = FindObjectOfType<WaypointContainer>();
        }

        void Update()
        {
            Patrol();
            if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance && character.GetIsAlive())
            {
                character.Move(navMeshAgent.desiredVelocity);
            }
            else
            {
                character.Move(Vector3.zero);
            }

        }

        void Patrol()
        {
            if (patrolPath != null)
            {
                Vector3 nextWaypointPos = patrolPath.transform.GetChild(nextWaypointIndex).position;
                SetDestination(nextWaypointPos);
                if (Vector3.Distance(transform.position, nextWaypointPos) <= stoppingDistance)
                {
                    CycleWaypoint();
                }
            }

        }

        private void CycleWaypoint()
        {
            nextWaypointIndex = (nextWaypointIndex + 1) % patrolPath.transform.childCount;
        }

        public int GetDamageToLifePoints()
        {
            return damageToLifePoints;
        }

        void AddNavMeshAgent()
        {
            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
            navMeshAgent.updateRotation = false;
            navMeshAgent.updatePosition = true;
            navMeshAgent.stoppingDistance = stoppingDistance;
            navMeshAgent.speed = navMeshSteeringSpeed;
            navMeshAgent.autoBraking = false;
            navMeshAgent.radius = navMeshRadius;
        }

        void SetDestination(Vector3 worldPos)
        {
            navMeshAgent.destination = worldPos;
        }
    }
}