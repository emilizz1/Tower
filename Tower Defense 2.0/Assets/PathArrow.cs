using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Towers.Enemies;

public class PathArrow : MonoBehaviour
{
    [SerializeField] float startTurningDistance;
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;

    WaypointContainer waypointContainer;
    int nextWaypointIndex;
    Vector3 destination;

    void Start()
    {
        waypointContainer = FindObjectOfType<WaypointContainer>();
        ResetArrow();
    }

    void Update()
    {
        CycleWaypoint();
        if (destination != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, movementSpeed);
            RotateArrow();
        }
    }

    void CycleWaypoint()
    {
        if (waypointContainer != null)
        {
            destination = waypointContainer.transform.GetChild(nextWaypointIndex).position;
            if (Vector3.Distance(transform.position, destination) <= startTurningDistance)
            {
                nextWaypointIndex = (nextWaypointIndex + 1) % waypointContainer.transform.childCount;
                if (nextWaypointIndex == 0)
                {
                    ResetArrow();
                }
            }
        }
    }

    void RotateArrow()
    {
        var _lookRotation = Quaternion.LookRotation((destination - transform.position).normalized);
        print(Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed));
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, rotationSpeed);
    }

    public void ArrowActive()
    {

    }

    public void ArrowDisabled()
    {

    }

    void ResetArrow()
    {
        nextWaypointIndex = 0;
        transform.position = waypointContainer.transform.GetChild(nextWaypointIndex).position;
    }
}
