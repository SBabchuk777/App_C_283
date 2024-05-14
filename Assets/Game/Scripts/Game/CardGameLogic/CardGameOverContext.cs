using System.Collections.Generic;
using System.Linq;
using CodeHub.OtherUtilities;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Game.CardGameLogic
{
    public class CardGameOverContext : MonoBehaviour
    {
        public HorizontalLayoutGroup HorizontalLayoutGroup;
        public GameObject Grid;
        public GameObject Win;
        public GameObject Counters;
        public GameObject OkBtn;

        public GameObject Win3;
        public GameObject Win2;
        public GameObject Win1;
        public GameObject Win0;

        public List<Image> Cards;
        public Sprite CloseIcon;

        [SerializeField] private AudioSource _winGame;

        [Inject] private PlayerDatabase _playerDatabase;

        public void Initialize()
        {
        }

        public void AddEndGame(List<CardData> winCards)
        {
            Grid.SetActive(false);
            Win.SetActive(true);
            Counters.SetActive(false);
            OkBtn.SetActive(true);

            AddWinByFindedPairs(winCards.Count);
            UpdateCardsUI(winCards);

            _winGame.Play();
        }

        private void AddWinByFindedPairs(int pairsCount)
        {
            if (pairsCount == 0)
            {
                Win0.gameObject.SetActive(true);
                _playerDatabase.IncreasePlayerBalance(300);
            }

            if (pairsCount == 1)
            {
                Win1.gameObject.SetActive(true);
                _playerDatabase.IncreasePlayerBalance(600);
            }

            if (pairsCount == 2)
            {
                Win2.gameObject.SetActive(true);
                _playerDatabase.IncreasePlayerBalance(900);

                HorizontalLayoutGroup.spacing = -340;//:(
            }

            if (pairsCount == 3)
            {
                Win3.gameObject.SetActive(true);
                _playerDatabase.IncreasePlayerBalance(1200);
            }
        }

        private void UpdateCardsUI(List<CardData> cards)
        {
            if (cards.Count == 0)
            {
                Cards.First().gameObject.SetActive(true);
                Cards.First().sprite = CloseIcon;
                return;
            }

            for (int i = 0; i < cards.Count; i++)
            {
                Cards[i].gameObject.SetActive(true);
                Cards[i].sprite = cards[i].OpenIcon;
            }
        }
    }
}