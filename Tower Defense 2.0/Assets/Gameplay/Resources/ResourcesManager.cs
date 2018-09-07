using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField] GameObject resourceImage;
    [Header("Resources")]
    [SerializeField] Text gold;
    [SerializeField] Text wood;
    [SerializeField] Text coal;
    [SerializeField] Image goldImage;
    [SerializeField] Image woodImage;
    [SerializeField] Image coalImage;
    [Header("ResourceLocations")]
    [SerializeField] Transform leftCard;
    [SerializeField] Transform rightCard;

    Sprite goldSprite;
    Sprite woodSprite;
    Sprite coalSprite;
    int currentGold = 2;
    int currentWood = 1;
    int currentCoal = 1;

    void Start()
    {
        updateText();
        goldSprite = goldImage.sprite;
        woodSprite = woodImage.sprite;
        coalSprite = coalImage.sprite;
    }

    void updateText()
    {
        gold.text = currentGold.ToString();
        wood.text = currentWood.ToString();
        coal.text = currentCoal.ToString();
    }

    public void AddResources(int amount, Sprite image)
    {
        if (goldSprite == image)
        {
            StartCoroutine(MoveResources(rightCard, image, amount, goldImage.transform, false));
        }
        else if (woodSprite == image)
        {
            StartCoroutine(MoveResources(rightCard, image, amount, woodImage.transform, false));
        }
        else if (coalSprite == image)
        {
            StartCoroutine(MoveResources(rightCard, image, amount, coalImage.transform, false));
        }
    }

    public bool CheckForResources(int goldAmount, int woodAmount, int coalAmount)
    {
        if (goldAmount <= currentGold && woodAmount <= currentWood && coalAmount <= currentCoal)
        {
            if(goldAmount > 0)
            {
                StartCoroutine(MoveResources(goldImage.transform, goldSprite, goldAmount, leftCard, true));
            }
            if(woodAmount > 0)
            {
                StartCoroutine(MoveResources(woodImage.transform, woodSprite, woodAmount, leftCard, true));
            }
            if (coalAmount > 0)
            {
                StartCoroutine(MoveResources(coalImage.transform, coalSprite, coalAmount, leftCard, true));
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddGold(int amount, Transform cardTransform)
    {
        StartCoroutine(MoveResources(cardTransform, goldSprite, amount, goldImage.transform, false));
    }

    IEnumerator MoveResources(Transform resTransform, Sprite resImage, int amount, Transform target, bool toPayResource)
    {
        int i = 0;
        while (i < amount)
        {
            i++;
            StartCoroutine(MoveResource(resTransform, resImage, target, toPayResource));
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    IEnumerator MoveResource(Transform resTransform, Sprite resImage, Transform target, bool toPayResource)
    {
        var createdResource = Instantiate(resourceImage, resTransform.position, Quaternion.identity, transform);
        createdResource.GetComponent<Image>().sprite = resImage;
        if(toPayResource)
        {
            PayOneResourceAmount(resImage);
        }
        while (Vector3.Distance(createdResource.transform.position, target.position) > 1f)
        {
             
            createdResource.transform.position = Vector3.MoveTowards(createdResource.transform.position, target.position, 10f);
            yield return new WaitForFixedUpdate();
        }
        if (!toPayResource)
        {
            AddOneResourceAmount(resImage);
        }
        Destroy(createdResource);
    }

    void AddOneResourceAmount(Sprite image)
    {
        if (goldSprite == image)
        {
            currentGold += 1;
        }
        else if (woodSprite == image)
        {
            currentWood += 1;
        }
        else if (coalSprite == image)
        {
            currentCoal += 1;
        }
        updateText();
    }

    void PayOneResourceAmount(Sprite image)
    {
        if (goldSprite == image)
        {
            currentGold -= 1;
        }
        else if (woodSprite == image)
        {
            currentWood -= 1;
        }
        else if (coalSprite == image)
        {
            currentCoal -= 1;
        }
        updateText();
    }
}
	
