using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    public class GiftExtraSpinButton : RewardedAdButton
    {
        [SerializeField] private GiftReset  gift;
        [SerializeField] private Animator   animator;
        [SerializeField] private GameObject giftScreen;
        [SerializeField] private GameObject animationScreen;
        [SerializeField] private GiftWheel  wheel;
        [SerializeField] private GameObject extraRewardButtons;

        protected override void Init()
        {
            extraRewardButtons = extraRewardButtons??transform.parent.gameObject;
            //wheel = wheel;//??giftScreen.GetComponentInChildren<GiftWheel>();
        }

        protected override void Reward()
        {
            gift.Unlock();
            wheel.ExtraSpin();

            extraRewardButtons.Deactivate();
            animator.SetTrigger("Wheel Screen");
        }
    }
}