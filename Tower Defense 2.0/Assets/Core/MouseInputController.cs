using UnityEngine;
using Towers.Scenes;

namespace Towers.Core
{
    public class MouseInputController : MonoBehaviour
    {
        public void MousePressed()
        {
            FindObjectOfType<GameplayPlayerInput>().MousePressed();
        }
    }
}
