using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] GameObject leftChoice;
    [SerializeField] GameObject rightChoice;
    [SerializeField] GameObject[] events;

	void Start ()
    {
        Instantiate(events[Random.Range(0, events.Length)], leftChoice.transform);
        Instantiate(events[Random.Range(0, events.Length)], rightChoice.transform);
        SetEventsActive(true);
	}

    void SetEventsActive(bool isActive)
    {
        leftChoice.SetActive(isActive);
        rightChoice.SetActive(isActive);
    }

    public void EventChosen(int choice)
    {
        if(choice == 0)
        {
            leftChoice.BroadcastMessage("Activated");
        }
        else if(choice == 1)
        {
            rightChoice.BroadcastMessage("Activated");
        }
        SetEventsActive(false);
    }
}
