using UnityEngine;
using System;

namespace Towers.Relics
{
    [Serializable]
    [CreateAssetMenu(menuName = ("Tower defense/Relic Holder"))]
    public class RelicHolder : ScriptableObject
    {
        [SerializeField] GameObject[] Relics;

        public GameObject[] GetAllRelics()
        {
            return Relics;
        }
    }
}
