using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

namespace PsychedelicGames.RotorBall.UI
{
    public class GiftDoubleRewardButton : RewardedAdButton
    {
        [SerializeField] private Animator   animator;
        [SerializeField] private GiftWheel  wheel;
        [SerializeField] private GameObject extraRewardButtons;

        protected override void Init()
        {
            extraRewardButtons = extraRewardButtons??transform.parent.gameObject;
            //wheel = wheel;//??giftScreen.GetComponentInChildren<GiftWheel>();
        }

        protected override void Reward()
        {
            wheel.DoublePrize();
            extraRewardButtons.Deactivate();
            animator.SetTrigger("Double Reward");
        }
    }
}
