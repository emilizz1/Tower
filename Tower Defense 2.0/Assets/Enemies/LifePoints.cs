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
            NewSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
            SceneManager.sceneLoaded += NewSceneLoaded;
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

        //To hide HP when in main menu and loading
        private void NewSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.buildIndex == 0 || arg0.buildIndex == 4)
            {
                GetComponentInChildren<Transform>().gameObject.SetActive(false);
                GetComponent<Image>().enabled = false;
            }
            else
            {
                GetComponentInChildren<Transform>().gameObject.SetActive(true);
                GetComponent<Image>().enabled = true;
            }
        }
    }
}