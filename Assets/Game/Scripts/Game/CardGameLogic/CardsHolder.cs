using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.Game.CardGameLogic
{
    public class CardsHolder : MonoBehaviour
    {
        public List<Card> Cards;
        public List<CardData> CardsData;

        private List<CardData> _availableCards;
        private CardData _targetCardData;

        public void Initialize()
        {
            _availableCards = new List<CardData>(CardsData);
        }

        public void PlayStartAnimation()
        {
            foreach (var card in Cards) card.PlayCloseOpenAnimation();
        }

        public void EnableButtonCards(bool enable, Card nonDisable= null)
        {
            foreach (var card in Cards)
            {
                if (nonDisable != null && card == nonDisable)
                    continue;

                card.Button.interactable = enable;
            }
        }

        public void RemoveAvailableCardData(CardData cardData) =>
            _availableCards.Remove(cardData);

        public CardData GetAvailableCardData()
        {
            int randomIndex = Random.Range(0, _availableCards.Count);
            return _availableCards[randomIndex];
        }

        public List<CardData> GetRandomCardsData() =>
            CardsData.OrderBy(x => Random.value).ToList();

        public void SetTargetCardData(CardData cardData) =>
            _targetCardData = cardData;

        public bool HasPair(CardData cardData) => cardData == _targetCardData;
    }
}