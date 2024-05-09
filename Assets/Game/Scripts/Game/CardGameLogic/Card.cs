using System;
using CodeHub.OtherUtilities;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Game.CardGameLogic
{
    public class Card : MonoBehaviour
    {
        public float RotateDuration;
        
        public Image CloseImage;
        public Image OpenImage;
        public Button Button;

        public CardData CardData;

        public event Action<Card> OnClick;

        private AnimationService _animationService;
        private Tween _animationTween;

        private void Start() =>
            _animationService = new AnimationService();

        private void OnDestroy() => 
            _animationTween?.Kill();

        public void ClickOnCard() => 
            OnClick?.Invoke(this);

        public void SetCardData(CardData cardData)
        {
            CardData = cardData;
            UpdateOpenImage();
        }

        public void PlayShakeAnimation() =>
            _animationService.StartShakeAnimation(gameObject);

        public void PlayFindPairsAnimation(float duration) =>
            _animationTween= gameObject.transform.DOPunchScale(new Vector3(0.5f, 0.5f), duration, 1)
                .SetLoops(2, LoopType.Yoyo);


        [ContextMenu("MakeCloseAnimation")]
        public void PlayCloseOpenAnimation()
        {
            OpenImage.gameObject.SetActive(false);
            CloseImage.gameObject.SetActive(true);

            CloseImage.transform.DORotate(new Vector3(90, 90, 0), RotateDuration / 2).OnComplete((() =>
            {
                CloseImage.gameObject.SetActive(false);
                OpenImage.gameObject.SetActive(true);

                CloseImage.transform.rotation = Quaternion.Euler(Vector3.zero);
                OpenImage.transform.rotation = Quaternion.Euler(new Vector3(90, 90, 0));
                _animationTween=  OpenImage.transform.DORotate(new Vector3(0, 0, 0), RotateDuration / 2);
            }));
        }

        private void UpdateOpenImage() =>
            OpenImage.sprite = CardData.FadeIcon;
    }
}