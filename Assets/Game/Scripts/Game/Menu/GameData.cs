using System;
using UnityEngine;

namespace Game.Scripts.Game.Menu
{
    [CreateAssetMenu(menuName = "Create GameSlotLocalData", fileName = "GameData", order = 0)]
    public class GameData : ScriptableObject
    {
        public Sprite NormalIcon;
        public Sprite FadeIcon;
    }
}