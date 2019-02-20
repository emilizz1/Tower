using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Towers.Scenes;

public class MouseInputController : MonoBehaviour
{
    public void MousePressed()
    {
        FindObjectOfType<GameplayPlayerInput>().MousePressed();
    }
}
