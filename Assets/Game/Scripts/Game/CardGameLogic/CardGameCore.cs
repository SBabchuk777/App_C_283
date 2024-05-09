using System;
using CodeHub.OtherUtilities;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Game.CardGameLogic
{
    public class CardGameCore : MonoBehaviour
    {
        public TMP_Text _pairsCountTxt;
        public Image TargetCardImage;

        public CardsHolder CardsHolder;
        public CardGameOverContext CardGameOverContext;

        [SerializeField] private AudioSource _rightPair;
        [SerializeField] private AudioSource _wrongPair;

        private bool _canClick;
        private AnimationService _animationService;
        private Tween _animationTween;

        private int _pairsCount;
        private int _maxPairsToWin;

        private float ActionDelay = 2f;

        private void OnDestroy()
        {
            _animationTween?.Kill();
            UnscribeEvents();
        }

        public void Initialize()
        {
            _animationService = new AnimationService();
            _pairsCount = 0;
            _maxPairsToWin = CardsHolder.Cards.Count;

            SubscribeEvents();

            CardsHolder.Initialize();
            CardGameOverContext.Initialize();

            ShuffleCardsData();
            RandomizeTargetCard();
        }

        public void StartGame()
        {
            CardsHolder.PlayStartAnimation();
            CardGameOverContext.GameTimer.StartTimer();
            CardGameOverContext.StartUpdateTimeTxt();

            _animationTween = DOVirtual.DelayedCall(ActionDelay, () => EnableCanClick(true));
        }

        private void ClickOnCard(Card card)
        {
            if (!_canClick)
                return;

            if (CardsHolder.HasPair(card.CardData))
                RightPair(card);
            else
                WrongPair(card);
        }

        private void ShuffleCardsData()
        {
            var randomCardsData = CardsHolder.GetRandomCardsData();
            for (int i = 0; i < CardsHolder.Cards.Count; i++)
            {
                CardsHolder.Cards[i].SetCardData(randomCardsData[i]);
            }
        }

        private void RandomizeTargetCard()
        {
            var availableCardData = CardsHolder.GetAvailableCardData();
            TargetCardImage.sprite = availableCardData.NormalIcon;
            CardsHolder.SetTargetCardData(availableCardData);
        }

        private void SubscribeEvents()
        {
            foreach (var card in CardsHolder.Cards) card.OnClick += ClickOnCard;
        }

        private void RightPair(Card card)
        {
            PlusRightPair();

            card.PlayFindPairsAnimation(1);
            _rightPair.Play();

            if (HasCollectMax) return;

            CardsHolder.EnableButtonCards(false, card);
            EnableCanClick(false);

            _animationTween = DOVirtual.DelayedCall(ActionDelay, () =>
            {
                CardsHolder.RemoveAvailableCardData(card.CardData);
                CardsHolder.EnableButtonCards(true);
                ShuffleCardsData();
                RandomizeTargetCard();
                EnableCanClick(true);
            });
        }

        private void WrongPair(Card card)
        {
            card.PlayShakeAnimation();
            _animationService.StartShakeAnimation(TargetCardImage.gameObject);
            CardsHolder.EnableButtonCards(false, card);

            EnableCanClick(false);
            _animationTween = DOVirtual.DelayedCall(ActionDelay, () =>
            {
                CardsHolder.EnableButtonCards(true);
                EnableCanClick(true);
            });

            _wrongPair.Play();
        }

        private void UnscribeEvents()
        {
            foreach (var card in CardsHolder.Cards) card.OnClick -= ClickOnCard;
        }

        private void PlusRightPair()
        {
            _pairsCount++;
            if (HasCollectMax)
            {
                CardGameOverContext.AddWin();
            }

            UpdatePairsCountTxt();
        }

        private bool HasCollectMax => _pairsCount == _maxPairsToWin;

        private void EnableCanClick(bool enable) =>
            _canClick = enable;

        private void UpdatePairsCountTxt() =>
            _pairsCountTxt.text = _pairsCount + "/" + _maxPairsToWin;
    }
}