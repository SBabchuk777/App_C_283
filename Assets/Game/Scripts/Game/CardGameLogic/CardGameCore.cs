using System;
using CodeHub.OtherUtilities;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Game.CardGameLogic
{
    public class CardGameCore : MonoBehaviour
    {
        public TMP_Text PairsCountTxt;
        public TMP_Text TriesCountTxt;

        public GameObject StartTutorial;
        public GameObject Counters;

        public CardsHolder CardsHolder;
        public CardGameOverContext CardGameOverContext;

        [SerializeField] private AudioSource _rightPair;
        [SerializeField] private AudioSource _wrongPair;

        private bool _canClick;
        private AnimationService _animationService;
        private Tween _animationTween;

        private int _pairsCount;
        private int _triesCount;
        private int _maxPairsToWin;
        private int _maxTries;

        private float ActionDelay = 1.2f;

        private void OnDestroy()
        {
            _animationTween?.Kill();
            UnscribeEvents();
        }

        public void Initialize()
        {
            _animationService = new AnimationService();
            _pairsCount = 0;
            _maxPairsToWin = 3;
            _maxTries = 4;

            SubscribeEvents();

            CardGameOverContext.Initialize();

            ShuffleCards();

            UpdatePairsCountTxt();
            UpdateTriesCountTxt();

            EnableCanClick(true);

            StartTutorial.SetActive(true);
            Counters.SetActive(false);
        }

        private void ClickOnCard(Card card)
        {
            if (!_canClick)
                return;

            StartTutorial.SetActive(false);
            Counters.SetActive(true);

            card.PlayOpenAnimation();

            if (CheckSameCard(card)) return;

            _triesCount++;

            if (CardsHolder.HasPair(card))
                RightPair(card);
            else
                WrongPair(card);

            UpdateTriesCountTxt();
            UpdatePairsCountTxt();
        }

        private bool CheckSameCard(Card card)
        {
            if (CardsHolder.SelectedCard != null) return false;

            CardsHolder.SetSelectedCardData(card);

            CardsHolder.EnableButtonCards(false);
            EnableCanClick(false);

            _animationTween = DOVirtual.DelayedCall(ActionDelay / 2, () =>
            {
                CardsHolder.EnableButtonCards(true);
                EnableCanClick(true);
            });
            return true;
        }

        private void ShuffleCards() => CardsHolder.ShuffleCardsBySpecial();

        private void SubscribeEvents()
        {
            foreach (var card in CardsHolder.Cards) card.OnClick += ClickOnCard;
        }

        private void RightPair(Card card)
        {
            _pairsCount++;

            _rightPair.Play();

            CardsHolder.EnableButtonCards(false);
            EnableCanClick(false);

            _animationTween = DOVirtual.DelayedCall(ActionDelay, () =>
            {
                CardsHolder.EnableButtonCards(true);

                card.PlayShakeAnimation();
                CardsHolder.SelectedCard.PlayShakeAnimation();
                CardsHolder.SetSelectedCardData(null);

                EnableCanClick(true);
                CheckEndGame();
            });
        }

        private void WrongPair(Card card)
        {
            _wrongPair.Play();

            CardsHolder.EnableButtonCards(false);

            EnableCanClick(false);
            _animationTween = DOVirtual.DelayedCall(ActionDelay, () =>
            {
                CardsHolder.EnableButtonCards(true);

                card.PlayCloseAnimation();
                CardsHolder.SelectedCard.PlayCloseAnimation();
                CardsHolder.SetSelectedCardData(null);

                EnableCanClick(true);
                CheckEndGame();
            });
        }

        private void UnscribeEvents()
        {
            foreach (var card in CardsHolder.Cards) card.OnClick -= ClickOnCard;
        }

        private void CheckEndGame()
        {
            if (HasTriesMax || HasCollectMax)
            {
                CardGameOverContext.AddWin();
            }

            UpdateTriesCountTxt();
            UpdatePairsCountTxt();
        }

        private bool HasCollectMax => _pairsCount == _maxPairsToWin;
        private bool HasTriesMax => _triesCount == _maxTries;

        private void EnableCanClick(bool enable) =>
            _canClick = enable;

        private void UpdatePairsCountTxt() =>
            PairsCountTxt.text = _pairsCount + "/" + _maxPairsToWin + " Pairs";

        private void UpdateTriesCountTxt() =>
            TriesCountTxt.text = _triesCount + "/" + _maxTries + " Tries";
    }
}