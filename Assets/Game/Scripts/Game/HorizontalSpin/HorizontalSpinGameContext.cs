using CodeHub.OtherUtilities;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace HorizontalSpin
{
    public class HorizontalSpinGameContext : MonoBehaviour
    {
        private PlayerDatabase _playerDatabase;

        [SerializeField] private HorizontalSpin _horizontalSpin;
        [SerializeField] private Button _openSpinBtn;

        [SerializeField] private int _rewardValue;

        [SerializeField] private GameObject _startDescription;
        [SerializeField] private GameObject _winDescription;
        [SerializeField] private GameObject _loseDescription;

        [SerializeField] private Button _spinBtn;
        [SerializeField] private Button _exitBtn;

        [SerializeField] private AudioSource _winSpin;
        [SerializeField] private AudioSource _looseSpin;

        [Inject]
        public void Construct(PlayerDatabase playerDatabase) =>
            _playerDatabase = playerDatabase;

        private void Start() =>
            UpdateOpenSpinBtn();

        public void TrySpin()
        {
            _startDescription.gameObject.SetActive(false);
            _spinBtn.gameObject.SetActive(false);
            _horizontalSpin.Spin(EndSpin);

            _playerDatabase.BonusClaimed();
            UpdateOpenSpinBtn();
        }

        private void EndSpin(ItemData itemData)
        {
            _exitBtn.gameObject.SetActive(true);

            if (itemData == null)
            {
                //endGamePanel
                _loseDescription.gameObject.SetActive(true);
                _looseSpin.Play();
                return;
            }

            GetReward();
        }

        private void GetReward()
        {
            _winDescription.gameObject.SetActive(true);

            _playerDatabase.IncreasePlayerBalance(_rewardValue);

            _winSpin.Play();
        }

        private void UpdateOpenSpinBtn() =>
            _openSpinBtn.interactable = _playerDatabase.HasBonusGame();
    }
}