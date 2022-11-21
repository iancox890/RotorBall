using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PsychedelicGames.RotorBall.Objectives.Achievments {
    using UI;
    public class AchievementButton : UIButton
    {
        [Tooltip("Must be exact")] [SerializeField] private string achievementName;
        override protected void OnClicked() {
            AchievementManager.Achieve(achievementName);
        }
    }
}
