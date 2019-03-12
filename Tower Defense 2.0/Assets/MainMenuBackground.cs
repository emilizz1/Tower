using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBackground : MonoBehaviour
{
    [SerializeField] Color[] bgColors;
    [SerializeField] float transitionSpeed = 0.1f;

    RawImage image;

    void Start()
    {
        image = GetComponent<RawImage>();
        StartCoroutine(ChangeBgColors());
    }

    IEnumerator ChangeBgColors()
    {
        Color changingTo = bgColors[Random.Range(0, bgColors.Length)];
        while (true)
        {
            image.color = Color.Lerp(image.color, changingTo, transitionSpeed);
            yield return new WaitForEndOfFrame();
            if(image.color == changingTo)
            {
                changingTo = bgColors[Random.Range(0, bgColors.Length)];
            }
            print("changing");
        }
    }
}
