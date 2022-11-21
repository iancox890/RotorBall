using UnityEngine;
using PsychedelicGames.RotorBall.Boosts;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Upgrades a given boost.
    /// </summary>
    public class BoostUpgradeButton : UIButton
    {
        [SerializeField] private Boost boost;
        [SerializeField] private TotalCurrencyText currencyText;

        public static event System.Action<Boost> OnDisplayed;

        protected override void OnClicked()
        {
            boost.Upgrade();
            currencyText.UpdateDisplay(boost.GetPreviousUpgradePrice());
        }

        protected override void OnEnabled() => OnDisplayed?.Invoke(boost);
    }
}
