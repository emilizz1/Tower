using UnityEngine;
using Towers.CardN;

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