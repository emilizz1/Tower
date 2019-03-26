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
            FindObjectOfType<CardsShowcase>().ShowcaseCards(FindObjectOfType<Deck>().GetLevelCards(), CardsShowcase.Showing.StartLevelCards);
            text.SetActive(true);
        }

        public void TurnOffAllShowcaseCards()
        {
            FindObjectOfType<CardsShowcase>().StartLevelAnimation();
            text.SetActive(false);
        }
    }
}