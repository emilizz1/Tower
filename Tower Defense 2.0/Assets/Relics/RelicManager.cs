using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Towers.Relics
{
    public class RelicManager : MonoBehaviour
    {
        [SerializeField] RelicHolder avaiableRelics;
        [SerializeField] GameObject[] relicSpots;

        int currentFreeSpot = 0;

        public void AddRelic(Relic relic)
        {
            GameObject relicToAdd = FindRelic(relic);
            Instantiate(relicToAdd, relicSpots[currentFreeSpot].transform.position, Quaternion.identity, relicSpots[currentFreeSpot].transform);
            currentFreeSpot++;
        }

        GameObject FindRelic(Relic relic)
        {
            foreach(GameObject myRelic in avaiableRelics.GetAllRelics())
            {
                if(myRelic.GetComponent<Relic>() == relic)
                {
                    return myRelic;
                }
            }
            return null;
        }
    }
}
