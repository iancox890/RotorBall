using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PsychedelicGames.RotorBall.Objectives.Achievments {

    public class AchieveSomething : MonoBehaviour
    {
        [Tooltip("Must be exact")] [SerializeField] private string achievementName;
        public void Achieve() {
            AchievementManager.Achieve(achievementName);
        }
    }
}