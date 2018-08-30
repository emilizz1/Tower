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
    int currentGold = 0;
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
            StartCoroutine(MoveResources(rightCard, image, amount, goldImage.transform));
        }
        else if (woodSprite == image)
        {
            StartCoroutine(MoveResources(rightCard, image, amount, woodImage.transform));
        }
        else if (coalSprite == image)
        {
            StartCoroutine(MoveResources(rightCard, image, amount, coalImage.transform));
        }
    }

    public bool CheckForResources(int goldAmount, int woodAmount, int coalAmount)
    {
        if (goldAmount <= currentGold && woodAmount <= currentWood && coalAmount <= currentCoal)
        {
            currentGold -= goldAmount;
            currentWood -= woodAmount;
            currentCoal -= coalAmount;
            updateText();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        updateText();
    }

    public Sprite GetResourceImages(bool gold = false, bool wood = false, bool coal = false)
    {
        if (gold)
        {
            return goldSprite;
        }
        else if (wood)
        {
            return woodSprite;
        }
        else if (coal)
        {
            return coalSprite;
        }
        return null;
    }

    IEnumerator MoveResources(Transform resTransform, Sprite resImage, int amount, Transform target)
    {
        int i = 0;
        while (i < amount)
        {
            i++;
            StartCoroutine(MoveResource(resTransform, resImage, target));
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    IEnumerator MoveResource(Transform resTransform, Sprite resImage, Transform target)
    {
        var createdResource = Instantiate(resourceImage, resTransform.position, Quaternion.identity, transform);
        createdResource.GetComponent<Image>().sprite = resImage;
        while (Vector3.Distance(createdResource.transform.position, target.position) > 1f)
        {
             
            createdResource.transform.position = Vector3.MoveTowards(createdResource.transform.position, target.position, 10f);
            yield return new WaitForFixedUpdate();
        }
        AddOneResourceAmount(resImage);
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
}
	
