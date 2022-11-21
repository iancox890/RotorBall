using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    using Boosts;

    /// <summary>
    /// Displays the info for a boost upgrade.
    /// </summary>
    public class BoostUpgradeDisplay : MonoBehaviour
    {
        [SerializeField] private Boost boostToDisplay;
        [Space]
        [SerializeField] private Text levelNumber;
        [SerializeField] private Text itemTitle;
        [SerializeField] private Text itemDescription;
        [SerializeField] private Text nextLevel;
        [SerializeField] private Text pricingCost;
        [Space]
        [SerializeField] private string maxed;
        [Space]
        [SerializeField] private CanvasGroup pricingGroup;
        [Space]
        [SerializeField] private Material activeMat;
        [Space]
        [SerializeField] private GameObject pricingConfirmation;
        [Space]
        [SerializeField] private Image[] levelBars;

        private void Awake()
        {
            itemTitle.text = boostToDisplay.Title;
            UpdateDisplay();
        }

        private void OnEnable()
        {
            Boost.OnUpgraded += UpdateDisplay;
            BoostUpgradeButton.OnDisplayed += UpdatePricingConfirmation;
        }

        private void OnDisable()
        {
            Boost.OnUpgraded -= UpdateDisplay;
            BoostUpgradeButton.OnDisplayed -= UpdatePricingConfirmation;
        }

        private void UpdatePricingConfirmation(Boost boost)
        {
            if (!boost.Equals(boostToDisplay))
            {
                pricingConfirmation.Deactivate();
                pricingGroup.gameObject.Activate();
            }
        }

        private void UpdateDisplay()
        {
            int level = boostToDisplay.GetLevel();

            itemDescription.text = boostToDisplay.Description;
            levelNumber.text = "Lvl. " + level.ToString();

            for (int i = 0; i < level; i++)
            {
                levelBars[i].material = activeMat;
            }

            if (level == Boost.MaxLevel)
            {
                nextLevel.text = maxed;
                pricingCost.text = maxed;

                pricingGroup.alpha = 1;
                pricingGroup.interactable = false;

                return;
            }
            else if (!boostToDisplay.IsPurchasable(0, false))
            {
                pricingGroup.alpha = 0.5f;
                pricingGroup.interactable = false;
            }
            else
            {
                pricingGroup.alpha = 1;
                pricingGroup.interactable = true;
            }

            float nextValue = boostToDisplay.GetBoostValue(level + 1);
            string nextValueString;

            if (nextValue < 1)
            {
                nextValueString = nextValue * 100 + "%";
            }
            else
            {
                nextValueString = nextValue.ToString();
            }

            nextLevel.text = nextValueString;
            pricingCost.text = boostToDisplay.GetRpValue(level).FormatValue();
        }
    }
}
