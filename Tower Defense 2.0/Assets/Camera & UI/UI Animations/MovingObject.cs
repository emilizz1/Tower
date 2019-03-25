using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    Transform myDestination;
    float moveSpeed;
    float rotationSpeed;

    void Update()
    {
        if (myDestination != null)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.parent.position.z);
            if (Vector3.Distance(transform.position, myDestination.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, myDestination.position, moveSpeed);
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

    public void GiveMovementInfo(Transform destination, float movSpeed, float rotSpeed)
    {
        myDestination = destination;
        moveSpeed = movSpeed;
        rotationSpeed = rotSpeed;
    }
}
