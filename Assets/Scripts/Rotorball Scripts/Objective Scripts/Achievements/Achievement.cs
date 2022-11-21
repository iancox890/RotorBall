using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PsychedelicGames.RotorBall.Objectives.Achievments
{
    [System.Serializable]
    public class Achievement
    {
        public string name;
        public bool isStat;
        public string statName;
        public int threshold;
        public int orderPosition;
    }
}
