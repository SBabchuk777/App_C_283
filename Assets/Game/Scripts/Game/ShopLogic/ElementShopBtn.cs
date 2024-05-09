using System;
using CodeHub.OtherUtilities;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Game.ShopLogic
{
    public class ElementShopBtn : MonoBehaviour
    {
        [SerializeField] private ShopElementData _elementData;

        [SerializeField] private Button _buyBtn;
        [SerializeField] private Button _selectBtn;

        [SerializeField] private GameObject _selectMark;

        public ShopElementData ElementData => _elementData;
        public Action<ElementShopBtn> onBuy;
        public Action<ElementShopBtn> onSelect;
        private PlayerDatabase _playerDatabase;

        [Inject]
        public void Construct(PlayerDatabase playerDatabase) =>
            _playerDatabase = playerDatabase;

        public void UpdateUI()
        {
            if (_elementData.HasOpen == false)
            {
                _buyBtn.gameObject.SetActive(true);
                _selectBtn.gameObject.SetActive(false);
                UpdateBuyBtnStatus();
            }
            else
            {
                _buyBtn.gameObject.SetActive(false);
                _selectBtn.gameObject.SetActive(true);
            }

            _selectMark.SetActive(false);
        }

        public void Select()
        {
            _buyBtn.gameObject.SetActive(false);
            _selectBtn.gameObject.SetActive(false);
            _selectMark.SetActive(true);
        }

        public void BuyEventsInvoke() => onBuy?.Invoke(this);

        public void SelectEventsInvoke() => onSelect?.Invoke(this);

        private void UpdateBuyBtnStatus() =>
            _buyBtn.interactable = _playerDatabase.PlayerBalance >= _elementData.Price;
    }
}