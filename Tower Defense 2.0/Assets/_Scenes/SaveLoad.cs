using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Towers.CardN;

namespace Towers.Scenes
{
    public class SaveLoad : MonoBehaviour
    {
        public static SaveLoad control;

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

        public void SavePlayerHolder(CardHolder playerHolder)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            bf.Serialize(file, playerHolder);
            file.Close();
        }

        public CardHolder LoadPlayerHolder()
        {
            if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
                CardHolder data = (CardHolder)bf.Deserialize(file);
                file.Close();
                return data;
            }
            return null;
        }
    }
}