using Towers.Events;
using UnityEngine;
using Towers.Core;

namespace Towers.Scenes.RunSelection
{
    public class RunSelectionPlayerInput : MonoBehaviour
    {
        State currentState;

        enum State
        {
            choosingEvent,
            buildingDeck,
            choosingLevel,
            nothing
        }

        EventManager eventManager;
        LevelSelectionManager levelSelectionManager;
        DeckBuilder deckBuilder;
        float screenWidth;

        void Start()
        {
            eventManager = FindObjectOfType<EventManager>();
            levelSelectionManager = FindObjectOfType<LevelSelectionManager>();
            deckBuilder = FindObjectOfType<DeckBuilder>();
            screenWidth = Screen.width;
            if (FindObjectOfType<NewRunPlus>().GetCardDraft())
            {
                currentState = State.buildingDeck;
            }
            else if (FindObjectOfType<SaveLoad>().LoadIntInfo("EventFinished") == 1)
            {
                currentState = State.choosingLevel;
                levelSelectionManager.ChangeReadyToSelect(true);
            }
            else
            {
                currentState = State.choosingEvent;
            }
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

        public void MousePressed()
        {
            if (Input.mousePosition.x < screenWidth / 2)
            {
                currentChoiceSelected(0);
            }
            if (Input.mousePosition.x > screenWidth / 2)
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
                case State.buildingDeck:
                    deckBuilder.CardChosen(choice);
                    if(deckBuilder.IsFinished())
                    {
                        currentState = State.choosingEvent;
                    }
                    break;
            }
        }
    }
}