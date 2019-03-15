using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    Vector3 myDestination = Vector3.zero;
    float moveSpeed;

    void Update()
    {
        if (myDestination != Vector3.zero)
        {
            if (Vector3.Distance(transform.position, myDestination) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, myDestination, moveSpeed);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void GiveMovementInfo(Vector3 destination, float moveSpeed)
    {
        myDestination = destination;
        this.moveSpeed = moveSpeed;
    }
}
