using System.Collections.Generic;
using UnityEngine;
using Towers.CardN;
using Towers.BuildingsN;

namespace Towers.CameraUI
{
    public class StartLevelDisplayer : MonoBehaviour
    {
        [SerializeField] GameObject text;
        
        CardHolder levelCards;

        void Start()
        {
            levelCards = FindObjectOfType<Deck>().GetLevelCards();
            FindObjectOfType<CardsShowcase>().ShowcaseCards(levelCards, CardsShowcase.Showing.StartLevelCards);
            text.SetActive(true);
        }

        public void TurnOffAllShowcaseCards()
        {
            FindObjectOfType<CardsShowcase>().ShowcaseCards(levelCards, CardsShowcase.Showing.StartLevelCards);
            text.SetActive(false);
        }
    }
}