using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    public class GiftWheelSpinButton : UIButton
    {
        [SerializeField] private GiftReset gift;
        [SerializeField] private GameObject speedupButton;
        [SerializeField] private string spinText = "SPIN!";
        [SerializeField] private string skipText = "SKIP!";

        private GiftWheel wheel;
        private Text text;
        private bool spun;

        protected override void Init()
        {
            wheel = FindObjectOfType<GiftWheel>();
            text = GetComponentInChildren<Text>();
            wheel.OnUnskippable.AddListener(()=>Interactable = false);
        }

        protected override void OnEnabled()
        {
            Interactable = true;
            spun = false;
            text.text = spinText;
            if (!gift.IsUnlocked())
            {
                gameObject.Deactivate();
                speedupButton.Activate();
            }
        }

        protected override void OnClicked()
        {
            if (wheel.CanSkip())
            {
                wheel.SkipSpin();
            } else if (gift.IsUnlocked())
            {
                wheel.Spin();
                spun = true;
                gift.Lock();
                text.text = skipText;
            }
        }
    }
}
