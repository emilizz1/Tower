using UnityEngine;

namespace Towers.Resources
{
    public class Resource : ScriptableObject
    {
        [SerializeField] Sprite sprite;

        public Sprite GetSprite()
        {
            return sprite;
        }

        public virtual void AddResource() { }
        public virtual void RemoveResource() { }
    } 
}
