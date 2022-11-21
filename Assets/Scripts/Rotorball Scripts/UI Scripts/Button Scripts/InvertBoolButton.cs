using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    using Gameplay;
    public class InvertBoolButton : BoolButton
    {
        [SerializeField] private Rotor rotor;
        protected override void OnClicked()
        {
            base.OnClicked();
            rotor.LoadControlSettings();
        }
    }
}