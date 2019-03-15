using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    Vector3 myDestination = Vector3.zero;
    float moveSpeed;
    float rotationSpeed;

    void Update()
    {
        if (myDestination != Vector3.zero)
        {
            if (Vector3.Distance(transform.position, myDestination) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, myDestination, moveSpeed);
                if(rotationSpeed != 0)
                {
                    transform.Rotate(0f, 0f, rotationSpeed);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void GiveMovementInfo(Vector3 destination, float movSpeed, float rotSpeed)
    {
        myDestination = destination;
        moveSpeed = movSpeed;
        rotationSpeed = rotSpeed;
    }
}
