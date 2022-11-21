using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    public class GiftContinueButton : UIButton
    {
        [SerializeField] Animator animator;
        [SerializeField] GameObject giftScreen;
        [SerializeField] GameObject animationScreen;

        protected override void OnClicked()
        {
            animator.SetTrigger("Wheel Screen");
        }
    }
}