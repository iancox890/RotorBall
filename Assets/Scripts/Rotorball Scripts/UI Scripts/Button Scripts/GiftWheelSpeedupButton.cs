using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    public class GiftWheelSpeedupButton : RewardedAdButton
    {
        [SerializeField] private GiftReset gift;
        [SerializeField] private GameObject spinButton;
        [SerializeField] private CountUp timer;

        override protected void Update()
        {
            base.Update();
            if (gift.IsUnlocked())
            {
                spinButton.Activate();
                gameObject.Deactivate();
            }
        }

        protected override void Reward()
        {
            print("Speeding up");

            System.TimeSpan initial = gift.TimeRemaining();
            long t1 = initial.Ticks;
            System.TimeSpan remaining = initial.Add(new System.TimeSpan(0,-gift.GiftSpeedupTimeInMinutes,0));
            long t2 = remaining.Ticks < 0L ? 0L : remaining.Ticks;
            timer.Count(t1,t2);
            
            gift.Speedup();
        }
    }
}