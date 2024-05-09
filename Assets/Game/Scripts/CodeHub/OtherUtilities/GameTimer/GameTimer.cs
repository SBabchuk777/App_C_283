using System;
using UnityEngine;

namespace CodeHub.OtherUtilities
{
    public class GameTimer : MonoBehaviour, IGameTimer
    {
        private float _secondsFromStart;
        private bool _isPause;

        public event Action OnEndTime;

        public float SecondsFromStart => _secondsFromStart;

        public void Initialize(int seconds)
        {
            _isPause = true;
            _secondsFromStart = seconds;
        }

        private void Update() => 
            CoreTimer();

        public string GetStringVisualizeTime(float time) => 
            TimeSpan.FromSeconds(time).ToString(@"mm\:ss");

        public void PauseTimer() => 
            _isPause = true;

        public void StartTimer() => 
            _isPause = false;

        private void CoreTimer()
        {
            if (_isPause == true)
                return;
            
            _secondsFromStart -= Time.deltaTime;

            if (_secondsFromStart <= 0)
            {
                PauseTimer();
                OnEndTime?.Invoke();
            }
        }
    }
}