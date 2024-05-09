using System;
using CodeHub.OtherUtilities;
using Game.Mephistoss.PanelMachine.Scripts;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Game.CardGameLogic
{
    public class CardGameOverContext : MonoBehaviour
    {
        public PanelMachine _panelMachine;
        public PanelBase _winPanel;
        public PanelBase _losePanel;
        public TMP_Text TimerTxt;

        [SerializeField] private AudioSource _winGame;
        [SerializeField] private AudioSource _loseGame;

        public int _rewardValue;
        public int _timeToEndGame;

        public GameTimer GameTimer;

        [Inject] private PlayerDatabase _playerDatabase;

        public void Initialize()
        {
            GameTimer.Initialize(_timeToEndGame);
            GameTimer.OnEndTime += AddLose;
        }

        public void StartUpdateTimeTxt() =>
            InvokeRepeating(nameof(UpdateTime), 0, 0.5f);


        private void OnDestroy() =>
            GameTimer.OnEndTime -= AddLose;

        public void AddWin()
        {
            _panelMachine.AddPanel(_winPanel);
            _playerDatabase.IncreasePlayerBalance(_rewardValue);
            GameTimer.PauseTimer();
            _winGame.Play();
        }

        private void UpdateTime() =>
            TimerTxt.text = GameTimer.GetStringVisualizeTime(GameTimer.SecondsFromStart);

        private void AddLose()
        {
            _panelMachine.AddPanel(_losePanel);

            _loseGame.Play();
        }
    }
}