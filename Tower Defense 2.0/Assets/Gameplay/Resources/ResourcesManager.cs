using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField] GameObject resourceImage;
    [SerializeField] float resourceMoveSpeed = 10f;
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

    public static ResourcesManager control;

    Sprite goldSprite;
    Sprite woodSprite;
    Sprite coalSprite;
    int currentGold = 3;
    int currentWood = 2;
    int currentCoal = 2;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != null)
        {
            Destroy(gameObject);
        }
    }

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
            StartCoroutine(GatherResources(rightCard, image, amount, goldImage.transform));
        }
        else if (woodSprite == image)
        {
            StartCoroutine(GatherResources(rightCard, image, amount, woodImage.transform));
        }
        else if (coalSprite == image)
        {
            StartCoroutine(GatherResources(rightCard, image, amount, coalImage.transform));
        }
    }

    public bool CheckForResources(int goldAmount, int woodAmount, int coalAmount)
    {
        if (goldAmount <= currentGold && woodAmount <= currentWood && coalAmount <= currentCoal)
        {
            if(goldAmount > 0)
            {
                StartCoroutine(PayResources(goldImage.transform, goldSprite, goldAmount));
            }
            if(woodAmount > 0)
            {
                StartCoroutine(PayResources(woodImage.transform, woodSprite, woodAmount));
            }
            if (coalAmount > 0)
            {
                StartCoroutine(PayResources(coalImage.transform, coalSprite, coalAmount));
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
        StartCoroutine(GatherResources(cardTransform, goldSprite, amount, goldImage.transform));
    }

    IEnumerator PayResources(Transform resTransform, Sprite resImage, int amount)
    {
        int i = 0;
        while (i < amount)
        {
            i++;
            StartCoroutine(PayResource(resTransform, resImage));
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    IEnumerator PayResource(Transform resTransform, Sprite resImage)
    {
        var createdResource = Instantiate(resourceImage, resTransform.position, Quaternion.identity, transform);
        var createdResImage = createdResource.GetComponent<Image>().color;
        createdResource.GetComponent<Image>().sprite = resImage;
        Vector3 target = new Vector3(createdResource.transform.position.x, createdResource.transform.position.y - 200f, 0f);
        PayOneResourceAmount(resImage);
        while (Vector3.Distance(createdResource.transform.position, target) > 1f)
        {
            createdResource.transform.position = Vector3.MoveTowards(createdResource.transform.position, target, resourceMoveSpeed / 3);
            createdResImage.a = createdResImage.a - 0.05f;
            createdResource.GetComponent<Image>().color = createdResImage;
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(createdResource);
    }

    IEnumerator GatherResources(Transform resTransform, Sprite resImage, int amount, Transform target)
    {
        int i = 0;
        while (i < amount)
        {
            i++;
            StartCoroutine(GatherResource(resTransform, resImage, target));
            yield return new WaitForSecondsRealtime(0.15f);
        }
    }

    IEnumerator GatherResource(Transform resTransform, Sprite resImage, Transform target)
    {
        var createdResource = Instantiate(resourceImage, resTransform.position, Quaternion.identity, transform);
        createdResource.GetComponent<Image>().sprite = resImage;
        while (Vector3.Distance(createdResource.transform.position, target.position) > 1f)
        {
            createdResource.transform.position = Vector3.MoveTowards(createdResource.transform.position, target.position, resourceMoveSpeed);
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
	
