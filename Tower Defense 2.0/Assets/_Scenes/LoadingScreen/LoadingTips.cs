using UnityEngine;
using UnityEngine.UI;

namespace Towers.Scenes.Loading
{
    public class LoadingTips : MonoBehaviour
    {
        [SerializeField] Text TipsText;
        [SerializeField] string[] loadingTips;

        void Start()
        {
            TipsText.text = loadingTips[Random.Range(0, loadingTips.Length)];
        }
    }
}
