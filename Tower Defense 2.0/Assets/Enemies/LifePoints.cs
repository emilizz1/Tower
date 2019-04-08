using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Towers.Core
{
    public class LifePoints : MonoBehaviour
    {
        [SerializeField] int startingLifepoints = 20;

        Text text;
        int lifePoints;

        void Start()
        {
            NewSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
            SceneManager.sceneLoaded += NewSceneLoaded;
            text = GetComponentInChildren<Text>();
        }

        public void ResetLifepoints()
        {
            lifePoints = startingLifepoints;
            UpdateLifePoints();
        }

        void UpdateLifePoints()
        {
            text.text = lifePoints.ToString();
        }

        public void DamageLifePoints(int amount)
        {
            lifePoints -= amount;
            if (lifePoints <= 0)
            {
                SceneManager.LoadScene(2);
            }
            UpdateLifePoints();
        }

        public int GetLifePoints()
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

        public void GiveNewLifepoints(int GivenLifepoints)
        {
            lifePoints = GivenLifepoints;
        }
    }
}