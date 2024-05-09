using Game.Mephistoss.PanelMachine.Scripts;
using UnityEngine;

namespace Game.Scripts.Game.CardGameLogic
{
    public class CardGameInitializer : MonoBehaviour
    {
        [SerializeField] private PanelMachine _panelMachine;
        [SerializeField] private PanelBase _rulesPanel;
        [SerializeField] private CardGameCore _cardGameCore;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            AddRulesPanel();
            _cardGameCore.Initialize();
        }

        private void AddRulesPanel() =>
            _panelMachine.AddPanel(_rulesPanel);
    }
}