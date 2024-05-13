using System;
using CodeHub.OtherUtilities;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Game.CardGameLogic
{
    public class DailyBonusContext : MonoBehaviour
    {
        [SerializeField] private GameObject _openDailyBtn;
        [SerializeField] private GameObject _timePanel;
        [SerializeField] private TMP_Text _timer;

        private PlayerDatabase _playerDatabase;

        private DateTime _bonusClaimedTime;

        [Inject]
        private void Construct(PlayerDatabase playerDatabase)
        {
            _playerDatabase = playerDatabase;
        }

        private void Start()
        {
            CheckTimer();
        }

        private void CheckTimer()
        {
            if (!_playerDatabase.HasBonusClaimData())
            {
                EnableDailyBtn(true);
                return;
            }

            if (_playerDatabase.HasBonusGame())
            {
                EnableDailyBtn(true);
                return;
            }

            SetBonusClaim();
            EnableDailyBtn(false);

            StartTimer();
        }

        public void SetBonusClaim() => 
            _bonusClaimedTime = _playerDatabase.GetLasBonusClaimedTime();

        public void EnableDailyBtn(bool enable)
        {
            _openDailyBtn.gameObject.SetActive(enable);
            _timePanel.gameObject.SetActive(!enable);
        }

        public void StartTimer()
        {
            CancelInvoke(nameof(UpdateTime));
            InvokeRepeating(nameof(UpdateTime), 0, 0.4f);
        }

        private void UpdateTime()
        {
            TimeSpan remainingTime = TimeSpan.FromHours(_playerDatabase.BonusCooldownHours) - (DateTime.Now - _bonusClaimedTime);
            //TimeSpan remainingTime = TimeSpan.FromSeconds(50) - (DateTime.Now - _bonusClaimedTime);
            _timer.text = remainingTime.ToString(@"hh\:mm\:ss");

            if (remainingTime.TotalSeconds <= 0)
            {
                CancelInvoke(nameof(UpdateTime));
                EnableDailyBtn(true);
            }
        }
    }
}