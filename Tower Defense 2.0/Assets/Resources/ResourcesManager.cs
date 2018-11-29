using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Towers.Resources
{
    public class ResourcesManager : MonoBehaviour
    {
        [SerializeField] GameObject resourceImage;
        [SerializeField] float resourceMoveSpeed = 10f;
        [Header("ResourceLinks")]
        [SerializeField] Text gold;
        [SerializeField] Text wood;
        [SerializeField] Text coal;
        [SerializeField] Image goldImage;
        [SerializeField] Image woodImage;
        [SerializeField] Image coalImage;

        Transform deliveringFrom;
        Sprite goldSprite;
        Sprite woodSprite;
        Sprite coalSprite;
        ResourceHolder resourceHolder;

        void Start()
        {
            goldSprite = goldImage.sprite;
            woodSprite = woodImage.sprite;
            coalSprite = coalImage.sprite;
            resourceHolder = FindObjectOfType<ResourceHolder>();
            updateText();
        }

        void Update()
        {
            if (resourceHolder == null)
            {
                resourceHolder = FindObjectOfType<ResourceHolder>();
                updateText();
            }
        }

        void updateText()
        {
            gold.text = resourceHolder.getCurrentGold().ToString();
            wood.text = resourceHolder.getCurrentWood().ToString();
            coal.text = resourceHolder.getCurrentCoal().ToString();
        }

        public void AddResources(int amount, Sprite image, Transform from = null)
        {
            if (from == null)
            {
                deliveringFrom = FindObjectOfType<ResourceCardChoice>().transform;
            }
            else
            {
                deliveringFrom = from;
            }
            if (goldSprite == image)
            {
                StartCoroutine(GatherResources(deliveringFrom, image, amount, goldImage.transform));
            }
            else if (woodSprite == image)
            {
                StartCoroutine(GatherResources(deliveringFrom, image, amount, woodImage.transform));
            }
            else if (coalSprite == image)
            {
                StartCoroutine(GatherResources(deliveringFrom, image, amount, coalImage.transform));
            }
        }

        public bool CheckForResources(int goldAmount, int woodAmount, int coalAmount)
        {
            if (goldAmount <= resourceHolder.getCurrentGold() && woodAmount <= resourceHolder.getCurrentWood() && coalAmount <= resourceHolder.getCurrentCoal())
            {
                if (goldAmount > 0)
                {
                    StartCoroutine(PayResources(goldImage.transform, goldSprite, goldAmount));
                }
                if (woodAmount > 0)
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
            createdResource.transform.localScale = resourceImage.transform.localScale;
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
                resourceHolder.AddGold(1);
            }
            else if (woodSprite == image)
            {
                resourceHolder.AddWood(1);
            }
            else if (coalSprite == image)
            {
                resourceHolder.AddCoal(1);
            }
            updateText();
        }

        void PayOneResourceAmount(Sprite image)
        {
            if (goldSprite == image)
            {
                resourceHolder.AddGold(-1);
            }
            else if (woodSprite == image)
            {
                resourceHolder.AddWood(-1);
            }
            else if (coalSprite == image)
            {
                resourceHolder.AddCoal(-1);
            }
            updateText();
        }
    }
}