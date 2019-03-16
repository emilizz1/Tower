using UnityEngine;

namespace Towers.Resources
{
    public class MovingResource : MonoBehaviour
    {
        Transform myDestination;
        float resourceMoveSpeed;
        Resource myResource;

        void Update()
        {
            if (myDestination != null)
            {
                if (Vector3.Distance(transform.position, myDestination.position) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, myDestination.position, resourceMoveSpeed);
                }
                else
                {
                    myResource.AddResource();
                    FindObjectOfType<ResourcesManager>().updateResourceText();
                    Destroy(gameObject);
                }
            }
        }

        public void GiveResourceMovementInfo(Transform destination, float moveSpeed, Resource resource)
        {
            myDestination = destination;
            resourceMoveSpeed = moveSpeed;
            myResource = resource;
        }
    }
}
