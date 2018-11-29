using UnityEngine;
using UnityEngine.UI;

namespace Towers.CameraUI
{
    public class EnemyText : MonoBehaviour
    {
        Text text;
        Cards card;

        void Start()
        {
            text = GetComponent<Text>();
            card = GetComponentInParent<Cards>();
        }

        void Update()
        {
            text.text = card.GetEnemyAmount().ToString() + " " + card.GetEnemyPrefab().name;
        }
    }
}