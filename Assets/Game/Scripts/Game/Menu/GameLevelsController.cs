using System.Collections.Generic;
using Prototype.SceneLoaderCore.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Game.Menu
{
    public class GameLevelsController : MonoBehaviour
    {
        private const int CurrentSlotIndex = 1;
        public List<GameData> SlotsGameData;
        public List<Image> SlotsImages;

        public GameData GetCurrentSlotData() => SlotsGameData[CurrentSlotIndex];
        public Image GetCurrentSlotImage() => SlotsImages[CurrentSlotIndex];

        private void Start() => UpdateSlotsImages();

        public void Right()
        {
            MoveSlotsElementsRight();

            UpdateSlotsImages();
        }

        public void Left()
        {
            MoveSlotsElementsLeft();

            UpdateSlotsImages();
        }

        public void PlayGame()
        {
            GameDataHolder.SetGameData(GetCurrentSlotData());
            SceneLoader.Instance.SwitchToScene("Game");
        }

        private void MoveSlotsElementsLeft()
        {
            var lastSlot = SlotsGameData[SlotsGameData.Count - 1];
            SlotsGameData.RemoveAt(SlotsGameData.Count - 1);
            SlotsGameData.Insert(0, lastSlot);
        }

        private void MoveSlotsElementsRight()
        {
            var firstSlot = SlotsGameData[0];
            SlotsGameData.RemoveAt(0);
            SlotsGameData.Add(firstSlot);
        }

        private void UpdateSlotsImages()
        {
            for (int i = 0; i < SlotsGameData.Count; i++)
            {
                SlotsImages[i].sprite = SlotsGameData[i].FadeIcon;
            }

            GetCurrentSlotImage().sprite = GetCurrentSlotData().NormalIcon;
        }
    }
}