﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WiningRewards : MonoBehaviour
{
    [SerializeField] int lifepointsRewarded = 5;
    [SerializeField] int[] resourcesAwarded;
    [SerializeField] GameObject rewards;
    [Header("Lifepoints")]
    [SerializeField] Text lifepointText;
    [Header("Resources")]
    [SerializeField] Sprite goldImage;
    [SerializeField] Sprite woodImage;
    [SerializeField] Sprite coalImage;
    [SerializeField] GameObject gold;
    [SerializeField] GameObject wood;
    [SerializeField] GameObject coal;

    public void PrepareRewards()
    {
        var resoureceManager = FindObjectOfType<ResourcesManager>();
        rewards.SetActive(true);
        lifepointText.text = lifepointsRewarded.ToString();
        FindObjectOfType<LifePoints>().DamageLifePoints(-lifepointsRewarded);
        gold.GetComponentInChildren<Text>().text = resourcesAwarded[0].ToString();
        resoureceManager.AddResources(resourcesAwarded[0], goldImage);
        wood.GetComponentInChildren<Text>().text = resourcesAwarded[1].ToString();
        resoureceManager.AddResources(resourcesAwarded[1], woodImage); 
        coal.GetComponentInChildren<Text>().text = resourcesAwarded[2].ToString();
        resoureceManager.AddResources(resourcesAwarded[2], coalImage);
    }
}
