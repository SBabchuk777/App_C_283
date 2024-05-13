using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.Game.CardGameLogic
{
    public class CardsHolder : MonoBehaviour
    {
        public List<Card> Cards;
        public List<CardData> CardsData;

        private Card _selectedCard;
        public Card SelectedCard => _selectedCard;

        public void EnableButtonCards(bool enable, Card nonDisable= null)
        {
            foreach (var card in Cards)
            {
                if (nonDisable != null && card == nonDisable)
                    continue;

                card.Button.interactable = enable;
            }
        }

        public void ShuffleCardsBySpecial()
        {
            var cardsData = GetRandomCardsData();
            var cardsUI = GetRandomCardsUI();
            for (int i = 0; i < cardsUI.Count; i++)
            {
                cardsUI[i].SetCardData(cardsData[i]);
            }
        }
        
        public List<CardData> GetRandomCardsData() =>
            CardsData.OrderBy(x => Random.value).ToList();
        
        public List<Card> GetRandomCardsUI() =>
            Cards.OrderBy(x => Random.value).ToList();

        public void SetSelectedCardData(Card cardData) =>
            _selectedCard = cardData;

        public bool HasPair(Card card) => card.CardData == _selectedCard.CardData;
    }
}