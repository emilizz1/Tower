using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(HealthSystem))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] float waypointWaitTime = 5f;
    [SerializeField] float damageToLifePoints = 1;

    WaypointContainer patrolPath;
    int nextWaypointIndex;
    Character character;
    
    void Start ()
    {
        character = GetComponent<Character>();
        patrolPath = FindObjectOfType<WaypointContainer>();
    }
	
	void Update ()
    {
        StartCoroutine(Patrol());
	}

    private IEnumerator Patrol()
    {
       while(patrolPath != null)
        {
            Vector3 nextWaypointPos = patrolPath.transform.GetChild(nextWaypointIndex).position;
            character.SetDestination(nextWaypointPos);
            if (Vector3.Distance(transform.position, nextWaypointPos) <= character.GetStoppingDistance())
            {
                CycleWaypoint(nextWaypointPos);
            }
            yield return new WaitForSeconds(waypointWaitTime);
        }
    }

    private void CycleWaypoint(Vector3 nextWaypointPos)
    {
        nextWaypointIndex = (nextWaypointIndex + 1) % patrolPath.transform.childCount;
    }

    public float GetDamageToLifePoints()
    {
        return damageToLifePoints;
    }
}
