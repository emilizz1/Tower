using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Towers.Enemies
{
    public class LifePoints : MonoBehaviour
    {
        [SerializeField] float startingLifepoints = 20;

        Text text;
        float lifePoints;

        void Start()
        {
            text = GetComponentInChildren<Text>();
            lifePoints = startingLifepoints;
            UpdateLifePoints();
        }

        void UpdateLifePoints()
        {
            text.text = lifePoints.ToString();
        }

        public void DamageLifePoints(float amount)
        {
            lifePoints -= amount;
            if (lifePoints <= 0f)
            {
                SceneManager.LoadScene(2);
            }
            UpdateLifePoints();
        }

        public float GetLifePoints()
        {
            return lifePoints;
        }
    }
}