using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSelectionPlayerInput : MonoBehaviour
{
    State currentState = State.choosingEvent;

    enum State
    {
        choosingEvent,
        nothing
    }

    EventManager eventManager;
    LevelSelectionManager levelSelectionManager;

    void Start()
    {
        eventManager = FindObjectOfType<EventManager>();
        levelSelectionManager = FindObjectOfType<LevelSelectionManager>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Choice Left"))
        {
            currentChoiceSelected(0);
        }
        if (Input.GetButtonDown("Choice Right"))
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
                break;
        }
    }
}
