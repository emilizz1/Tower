using Towers.Scenes.RunSelection;
using UnityEngine;

namespace Towers.Events
{
    public class EventManager : MonoBehaviour
    {
        [SerializeField] GameObject leftChoice;
        [SerializeField] GameObject rightChoice;

        void Start()
        {
            LevelSelection myEvent = FindObjectOfType<LevelSelectionManager>().GetCurrentLevel();
            Instantiate(myEvent.GetEvents()[Random.Range(0, myEvent.GetEvents().Length)], leftChoice.transform);
            Instantiate(myEvent.GetEvents()[Random.Range(0, myEvent.GetEvents().Length)], rightChoice.transform);
            SetEventsActive(true);
        }

        void SetEventsActive(bool isActive)
        {
            leftChoice.SetActive(isActive);
            rightChoice.SetActive(isActive);
        }

        public void EventChosen(int choice)
        {
            if (choice == 0)
            {
                leftChoice.BroadcastMessage("Activated");
            }
            else if (choice == 1)
            {
                rightChoice.BroadcastMessage("Activated");
            }
            SetEventsActive(false);
        }
    }
}