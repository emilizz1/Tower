using UnityEngine;
using UnityEngine.AI;

namespace Towers.Enemies
{
    [RequireComponent(typeof(HealthSystem))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] int damageToLifePoints = 1;

        [Header("Animator Settings")]
        [SerializeField] RuntimeAnimatorController animatorController;
        [SerializeField] AnimatorOverrideController animatorOverrideController;
        [SerializeField] Avatar characterAvatar;

        [Header("Nav Mesh Agent")]
        [SerializeField] float stoppingDistance = 1f;
        [SerializeField] float navMeshSteeringSpeed = 1f;
        [SerializeField] float navMeshRadius = 0.25f;

        [Header("Movement")]
        [SerializeField] float moveSpeedMulyiplier = 1f;
        [SerializeField] float animationSpeedMultiplier = 1f;
        [SerializeField] float movingTurnSpeed = 360;
        [SerializeField] float stationaryTurnSpeed = 180;
        [SerializeField] float moveThreshold = 1f;

        [Header("Captule Collider")]
        [SerializeField] Vector3 colliderCenter = new Vector3(0f, 1.5f, 0f);
        [SerializeField] float collidererRadius = 0.25f;
        [SerializeField] float colliderHeight = 2.5f;

        bool isAlive = true;

        NavMeshAgent navMeshAgent;
        WaypointContainer patrolPath;
        int nextWaypointIndex;
        float turnAmount;
        float forwardAmount;
        Animator animator;
        Rigidbody myRigidbody;

        private void Awake()
        {
            AddRequiredComponents();
        }

        void Start()
        {
            patrolPath = FindObjectOfType<WaypointContainer>();
        }

        void Update()
        {
            Patrol();
            if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance && isAlive)
            {
                Move(navMeshAgent.desiredVelocity);
            }
            else
            {
                Move(Vector3.zero);
            }
        }

        void AddRequiredComponents()
        {
            var capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
            capsuleCollider.center = colliderCenter;
            capsuleCollider.radius = collidererRadius;
            capsuleCollider.height = colliderHeight;

            myRigidbody = gameObject.AddComponent<Rigidbody>();
            myRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 0.5f; // if wanted to change create [Header("Audio")] [SerializeField] float audioSourceSpatialBlend = 0.5f;

            animator = gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = animatorController;
            animator.avatar = characterAvatar;

            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
            navMeshAgent.updateRotation = false;
            navMeshAgent.updatePosition = true;
            navMeshAgent.stoppingDistance = 0.1f;
            navMeshAgent.speed = navMeshSteeringSpeed;
            navMeshAgent.autoBraking = false;
            navMeshAgent.radius = navMeshRadius;
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
            if(nextWaypointIndex == 0)
            {
                stoppingDistance = 0.1f;
            }
        }

        public int GetDamageToLifePoints()
        {
            return damageToLifePoints;
        }

        void SetDestination(Vector3 worldPos)
        {
            navMeshAgent.destination = worldPos;
        }

        public void Move(Vector3 movement)
        {
            SetFoweardAndTurn(movement);
            ApplyExtraTurnRotation();
            UpdateAnimator();
        }

        void SetFoweardAndTurn(Vector3 movement)
        {
            if (movement.magnitude > moveThreshold)
            {
                movement.Normalize();
            }
            var localMove = transform.InverseTransformDirection(movement);
            turnAmount = Mathf.Atan2(localMove.x, localMove.z);
            forwardAmount = localMove.z;
        }

        void UpdateAnimator()
        {
            animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
            animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
            animator.speed = animationSpeedMultiplier;
        }

        void ApplyExtraTurnRotation()
        {
            float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
            transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
        }

        public void Kill()
        {
            isAlive = false;
        }

        private void OnAnimatorMove()
        {
            if (Time.deltaTime > 0)
            {
                Vector3 velocity = (animator.deltaPosition * moveSpeedMulyiplier) / Time.deltaTime;
                velocity.y = myRigidbody.velocity.y;
                myRigidbody.velocity = velocity;
            }
        }
    }
}