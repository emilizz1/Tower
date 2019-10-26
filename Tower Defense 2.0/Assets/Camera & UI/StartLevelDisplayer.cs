using UnityEngine;
using Towers.CardN;

namespace Towers.CameraUI
{
    public class StartLevelDisplayer : MonoBehaviour
    {
        [SerializeField] GameObject text;
        
        CardHolder levelCards;
        GameObject level;

        void Start()
        {
            FindObjectOfType<CardsShowcase>().ShowcaseCards(FindObjectOfType<Deck>().GetLevelCards(), CardsShowcase.Showing.StartLevelCards);
            level = GameObject.Find("Level");
            level.SetActive(false);
            text.SetActive(true);
        }

        public void TurnOffAllShowcaseCards()
        {
            FindObjectOfType<CardsShowcase>().StartLevelAnimation();
            text.SetActive(false);
            level.SetActive(true);
        }
    }
}