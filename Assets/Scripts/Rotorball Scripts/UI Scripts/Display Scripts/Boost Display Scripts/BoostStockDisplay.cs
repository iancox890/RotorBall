using UnityEngine;
using UnityEngine.UI;
// using System;
using System.Collections.Generic;

namespace PsychedelicGames.RotorBall.UI
{
    using Boosts;

    /// <summary>
    /// Displays the stock information for a boost.
    /// </summary>
    public class BoostStockDisplay : MonoBehaviour
    {
        [SerializeField] private Boost boost;
        [Space]
        [SerializeField] private Text title;
        [SerializeField] private Text description;
        [SerializeField] private Text stock;
        [SerializeField] private Text price;
        [Space]
        [SerializeField] private List<CanvasGroup> disableCGsIfBoostMaxed;

        public void UpdateDisplay()
        {
            stock.text = boost.GetStock().ToString() + " OWNED";

            if (boost.IsPurchasable(1, false) == false || boost.IsMaxed(1)) {
                disableCGsIfBoostMaxed.ForEach((CanvasGroup cg)=>{cg.alpha = 0.5f;cg.interactable = false;});
            }
            else {
                disableCGsIfBoostMaxed.ForEach((CanvasGroup cg)=>{cg.alpha = 1;cg.interactable = true;});
            }
        }

        private void Awake()
        {
            title.text = boost.Title;
            description.text = boost.Description;
            price.text = boost.RpForStock.FormatValue();
        }

        private void OnEnable()
        {
            Boost.OnStocked += UpdateDisplay;
        }

        private void Start() {
            UpdateDisplay();
        }

        private void OnDisable() => Boost.OnStocked -= UpdateDisplay;
    }
}
