using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    using Gameplay;

    /// <summary>
    /// Controls the sensitivity slider
    /// </summary>
    public class SensitivitySliderButton : SliderButton
    {
        [SerializeField] Rotor rotor;

        protected override void Save()
        {
            base.Save();
            rotor.LoadControlSettings();
        }
    }
}
