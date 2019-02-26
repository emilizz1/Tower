using UnityEngine;
using UnityEngine.UI;

namespace Towers.Scenes
{
    public class LoadingTips : MonoBehaviour
    {
        [SerializeField] Sprite[] loadingTips;

        void Start()
        {
            //GetComponent<Image>().sprite = loadingTips[Random.Range(0, loadingTips.Length)];
        }
    }
}
