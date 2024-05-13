using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Game.Menu
{
    public class SlotInitializer : MonoBehaviour
    {
        public List<SlotDataHolder> Slots;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            var currentData = GameDataHolder.GameData;
            foreach (var slot in Slots)
            {
                if (slot.GameData == currentData) slot.gameObject.SetActive(true);
            }
        }
    }
}