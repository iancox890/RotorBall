using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PsychedelicGames.RotorBall.Animations
{
    using Boosts;
    public class BoostInfoPanel : MonoBehaviour
    {
        [SerializeField] private Boost boost;
        public Boost GetBoost()
        {
            return boost;
        }
    }
}
