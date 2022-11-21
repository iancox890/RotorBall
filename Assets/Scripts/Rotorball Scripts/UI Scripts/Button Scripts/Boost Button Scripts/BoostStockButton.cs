using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    using Boosts;

    /// <summary>
    /// Updates the stock for a given boost.
    /// </summary>
    public class BoostStockButton : UIButton
    {
        [SerializeField] private Boost boost;
        [SerializeField] private TotalCurrencyText currencyText;

        override protected void Init() => boost.OnMaxed+=()=>Interactable=false;

        protected override void OnClicked()
        {
            boost.AddStock();
            currencyText.UpdateDisplay(boost.RpForStock);
        }
    }
}
