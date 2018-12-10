using UnityEngine;

namespace Towers.Resources
{
    public class MovingResource : MonoBehaviour
    {
        Vector3 myDestination = Vector3.zero;
        float resourceMoveSpeed;
        Resource myResource;

        void Update()
        {
            if (myDestination != Vector3.zero)
            {
                if (Vector3.Distance(transform.position, myDestination) > 1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, myDestination, resourceMoveSpeed);
                }
                else
                {
                    myResource.AddResource();
                    Destroy(gameObject);
                }
            }
        }

        public void GiveResourceMovementInfo(Vector3 destination, float moveSpeed, Resource resource)
        {
            myDestination = destination;
            resourceMoveSpeed = moveSpeed;
            myResource = resource;
        }
    }
}
