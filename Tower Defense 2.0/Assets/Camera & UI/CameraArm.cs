using UnityEngine;

namespace Towers.CameraUI
{
    public class CameraArm : MonoBehaviour
    {
        [SerializeField] Vector3 LevelView = new Vector3(0f, 45f, -20f);
        [SerializeField] Vector3 BattlefieldView = new Vector3(0f, 27.5f, -15f);
        [SerializeField] Vector3 buildingView = new Vector3(0f, 15f, -7.5f);
        Vector3 velocity = Vector3.zero;
        Vector3 newPosition;


        void Start()
        {
            newPosition = transform.position;
        }

        void Update()
        {
            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, 0.5f);
        }

        public void ViewBuilding(Vector3 position)
        {
            newPosition = position + buildingView;
        }

        public void ViewBattleField()
        {
            newPosition = BattlefieldView;
        }

        public void Viewlevel()
        {
            newPosition = LevelView;
        }
    }
}