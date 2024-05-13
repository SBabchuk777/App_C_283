using CodeHub.OtherUtilities;
using Game.Mephistoss.PanelMachine.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Game.CardGameLogic
{
    public class CardGameOverContext : MonoBehaviour
    {
        public PanelMachine _panelMachine;
        public PanelBase _winPanel;
        public PanelBase _losePanel;

        [SerializeField] private AudioSource _winGame;
        [SerializeField] private AudioSource _loseGame;

        public int _rewardValue;


        [Inject] private PlayerDatabase _playerDatabase;

        public void Initialize()
        {
        }
        
        public void AddWin()
        {
            _panelMachine.AddPanel(_winPanel);
            _playerDatabase.IncreasePlayerBalance(_rewardValue);

            _winGame.Play();
        }

        private void AddLose()
        {
            _panelMachine.AddPanel(_losePanel);

            _loseGame.Play();
        }
    }
}