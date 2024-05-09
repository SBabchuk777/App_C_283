using UnityEngine;

namespace Game.Scripts.Game.CardGameLogic
{
    [CreateAssetMenu(menuName = "CardGame/CardData", fileName = "CardData", order = 0)]
    public class CardData : ScriptableObject
    {
        public Sprite NormalIcon;
        public Sprite FadeIcon;
    }
}