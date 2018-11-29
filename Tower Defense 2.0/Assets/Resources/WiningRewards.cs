using Towers.BuildingsN;
using Towers.CardN;
using Towers.Enemies;
using Towers.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace Towers.Resources
{
    public class WiningRewards : MonoBehaviour
    {
        [SerializeField] GameObject rewards;
        [Header("Lifepoints")]
        [SerializeField] Text lifepointText;
        [SerializeField] int lifepointsRewarded = 5;
        [Header("Resources")]
        [SerializeField] int[] resourcesAwarded;
        [SerializeField] Sprite goldImage;
        [SerializeField] Sprite woodImage;
        [SerializeField] Sprite coalImage;
        [SerializeField] GameObject gold;
        [SerializeField] GameObject wood;
        [SerializeField] GameObject coal;
        [Header("Level")]
        [SerializeField] int currentLevel;
        [Header("Cards")]
        [SerializeField] GameObject cardRewards;
        [SerializeField] Card cardAddedToDeck;
        [SerializeField] Card[] cardsAddedToAddables;

        public void PrepareRewards()
        {
            var resoureceManager = FindObjectOfType<ResourcesManager>();
            rewards.SetActive(true);
            lifepointText.text = lifepointsRewarded.ToString();
            FindObjectOfType<LifePoints>().DamageLifePoints(-lifepointsRewarded);
            gold.gameObject.GetComponentInChildren<Text>().text = resourcesAwarded[0].ToString();
            resoureceManager.AddResources(resourcesAwarded[0], goldImage, gold.transform);
            wood.gameObject.GetComponentInChildren<Text>().text = resourcesAwarded[1].ToString();
            resoureceManager.AddResources(resourcesAwarded[1], woodImage, wood.transform);
            coal.gameObject.GetComponentInChildren<Text>().text = resourcesAwarded[2].ToString();
            resoureceManager.AddResources(resourcesAwarded[2], coalImage, coal.transform);
            FindObjectOfType<LevelCounter>().LevelFinished(currentLevel);
            if (cardAddedToDeck != null)
            {
                cardRewards.SetActive(true);
                FindObjectOfType<CardHolders>().AddPlayerCard(cardAddedToDeck);
                GetComponentInChildren<ShowcaseCard>().PutInformation(cardAddedToDeck, GetBuildingLevel());
            }
            else
            {
                cardRewards.SetActive(false);
            }
            foreach (Card card in cardsAddedToAddables)
            {
                FindObjectOfType<CardHolders>().AddAddableCard(card);
            }
        }

        int GetBuildingLevel()
        {
            int buildingLevel = 0;
            Buildings currentlyLooking = cardAddedToDeck.GetPrefabs().GetBuilding(0);
            foreach (Card card in FindObjectOfType<CardHolders>().GetAllPlayerCards())
            {
                if (currentlyLooking == card.GetPrefabs().GetBuilding(0))
                {
                    buildingLevel++;
                }
            }
            return buildingLevel + 1;
        }
    }
}