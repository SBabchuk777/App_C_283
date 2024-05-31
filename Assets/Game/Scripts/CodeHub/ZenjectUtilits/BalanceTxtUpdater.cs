using System.Globalization;
using CodeHub.OtherUtilities;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Scripts.CodeHub.ZenjectUtilits
{
    [RequireComponent(typeof(TMP_Text))]
    public class BalanceTxtUpdater : MonoBehaviour
    {
        private PlayerDatabase _playerDatabase;
        private TMP_Text _balanceTxt;

        [Inject]
        public void Construct(PlayerDatabase playerDatabase) =>
            _playerDatabase = playerDatabase;

        private void Start()
        {
            _balanceTxt = GetComponent<TMP_Text>();
            _playerDatabase.OnPlayerBalanceChange += UpdateBalanceTxt;
            UpdateBalanceTxt(_playerDatabase.PlayerBalance);
        }

        private void OnDestroy() => 
            _playerDatabase.OnPlayerBalanceChange -= UpdateBalanceTxt;

        private void UpdateBalanceTxt(int count)
        {
            var nfi = new NumberFormatInfo
            {
                NumberGroupSeparator = " ",
                NumberDecimalDigits = 0
            };
    
            _balanceTxt.text = count.ToString("N", nfi);
        }
    }
}