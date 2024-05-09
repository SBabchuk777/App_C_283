using UnityEngine;
using UnityEngine.UI;

namespace HorizontalSpin
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        public int X { get; private set; }
        public int Y { get; private set; }
        public ItemData ItemData { get; private set; }

        public void Initialize( ItemData itemData)
        {
            ItemData = itemData;

            _icon.transform.localScale = Vector3.one;
        }

        public void SetItem(ItemData itemData) => 
            ItemData = itemData;

        public void UpdateIcon() => 
            _icon.sprite = ItemData.Icon;

        public Image GetIcon() => 
            _icon;
    }
}