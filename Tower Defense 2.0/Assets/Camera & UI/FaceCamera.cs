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
            transform.LookAt(cameraToLookAt.transform);
        }
    }
}