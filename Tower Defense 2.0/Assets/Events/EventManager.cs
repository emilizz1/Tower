using Towers.Core;
using Towers.Scenes.RunSelection;
using UnityEngine;

namespace Towers.Events
{
    public class EventManager : MonoBehaviour
    {
        [SerializeField] GameObject leftChoice;
        [SerializeField] GameObject rightChoice;

        public void PrepareEvents()
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
            gameObject.SetActive(isActive);
        }

        public void EventChosen(int choice)
        {
            if (choice == 0)
            {
                leftChoice.BroadcastMessage("Activated");
                rightChoice.SetActive(false);

            }
            else if (choice == 1)
            {
                rightChoice.BroadcastMessage("Activated");
                leftChoice.SetActive(false);
            }
            FindObjectOfType<SaveLoad>().SaveIntInfo("EventFinished", 1);
        }
    }
}