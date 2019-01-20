using Towers.Events;
using UnityEngine;

namespace Towers.Scenes.RunSelection
{
    public class RunSelectionPlayerInput : MonoBehaviour
    {
        State currentState = State.choosingEvent;

        enum State
        {
            choosingEvent,
            choosingLevel,
            nothing
        }

        EventManager eventManager;
        LevelSelectionManager levelSelectionManager;
        float screenWidth;

        void Start()
        {
            eventManager = FindObjectOfType<EventManager>();
            levelSelectionManager = FindObjectOfType<LevelSelectionManager>();
            screenWidth = Screen.width;
        }

        void Update()
        {
            if (Input.GetButtonDown("Choice Left") || (Input.GetMouseButtonDown(0) && Input.mousePosition.x < screenWidth / 2))
            {
                currentChoiceSelected(0);
            }
            if (Input.GetButtonDown("Choice Right") || (Input.GetMouseButtonDown(0) && Input.mousePosition.x > screenWidth / 2))
            {
                currentChoiceSelected(1);
            }
        }

        void currentChoiceSelected(int choice)
        {
            switch (currentState)
            {
                case State.choosingEvent:
                    eventManager.EventChosen(choice);
                    levelSelectionManager.ChangeReadyToSelect(true);
                    currentState = State.choosingLevel;
                    break;
            }
        }
    }
}