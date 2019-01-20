using UnityEngine;

namespace Towers.CameraUI
{
    public class FaceCamera : MonoBehaviour
    {
        Camera cameraToLookAt;

        void Start()
        {
            cameraToLookAt = Camera.main;
        }

        void LateUpdate()
        {
            var cameraXpos = cameraToLookAt.transform.position.x;
            cameraXpos = transform.position.x;
            transform.LookAt(new Vector3(cameraXpos, cameraToLookAt.transform.position.y, cameraToLookAt.transform.position.z));
        }
    }
}