using System;
using Game.Mephistoss.PanelMachine.Scripts;
using Slots.Game.Values;
using Tools.Core.UnityAdsService.Scripts;
using UnityEngine;

namespace Game.Scripts.Game
{
    public class AdsRewardLogic : MonoBehaviour
    {
        public UnityAdsButton AdsBtn;
        public PanelMachine PanelMachine;
        public AudioSource WinAudio;
        public int Reward;

        private void Start()
        {
            AdsBtn.OnCanGetReward += GetReward;
        }

        private void OnDestroy()
        {
            AdsBtn.OnCanGetReward -= GetReward;
        }

        public void GetReward()
        {
            Wallet.AddMoney(Reward);
            PanelMachine.CloseLastPanel();
            WinAudio.Play();
        }
    }
}