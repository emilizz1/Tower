using UnityEngine;
using Towers.Scenes;

namespace Towers.CameraUI {
    public class MouseInputController : MonoBehaviour
    {
        public void MousePressed()
        {
            FindObjectOfType<GameplayPlayerInput>().MousePressed();
        }
    }
}
