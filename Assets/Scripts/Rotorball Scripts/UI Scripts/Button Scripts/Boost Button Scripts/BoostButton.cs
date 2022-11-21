using UnityEngine;
using PsychedelicGames.RotorBall.Boosts;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Activates a given boost.
    /// </summary>
    public class BoostButton : UIButton
    {
        [SerializeField] private Boost boost;
        private BoostManager boostManager;

        protected override void Init() => boostManager = FindObjectOfType<BoostManager>();
        protected override void OnClicked() => boostManager.Apply(boost);
    }
}
