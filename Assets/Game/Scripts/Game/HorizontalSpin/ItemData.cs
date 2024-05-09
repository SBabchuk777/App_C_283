using UnityEngine;

namespace HorizontalSpin
{
    [CreateAssetMenu(menuName = "HorizontalSpin/ItemData", fileName = "ItemData", order = 0)]
    public class ItemData : ScriptableObject 
    {
        public Sprite _icon;

        public Sprite Icon => _icon;
    }
}