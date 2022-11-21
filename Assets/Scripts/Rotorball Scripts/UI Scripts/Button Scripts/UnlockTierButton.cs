using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    using LevelManagement;
    using Files;
    public class UnlockTierButton : UIButton
    {
        [SerializeField] TierData tierData;
        [SerializeField] int price;
        [SerializeField] TierDataDisplay tierDataDisplay;
        private bool _canAfford { get => PlayerFile.GetFile().RotorPoints <= price; }

        protected override void Init()
        {
            base.Init();
        }

        override protected void OnEnabled() {
            Interactable = _canAfford;
        }

        protected override void OnClicked()
        {
            PlayerFile pf = PlayerFile.file;
            if (pf.RotorPoints >= price)
            {
                pf.RotorPoints -= price;
                PlayerFile.file = pf;
                PlayerFile.SaveFile();
                tierData.IsLocked = false;
                tierDataDisplay.CheckIfLocked();
            }
        }
    }
}