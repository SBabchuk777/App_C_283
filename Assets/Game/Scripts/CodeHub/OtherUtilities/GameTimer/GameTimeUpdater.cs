using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeHub.OtherUtilities
{
    [RequireComponent(typeof(TMP_Text))]
    public class GameTimeUpdater : MonoBehaviour
    {
        public float RepeatTime;

        private IGameTimer _gameTimer;
        private TMP_Text _txt;

        [Inject]
        public void Construct(IGameTimer gameTimer) =>
            _gameTimer = gameTimer;

        private void Start()
        {
            _txt = GetComponent<TMP_Text>();
            InvokeRepeating(nameof(UpdateTxt), 0, RepeatTime);
        }

        private void UpdateTxt() =>
            _txt.text = _gameTimer.GetStringVisualizeTime(_gameTimer.SecondsFromStart);
    }
}