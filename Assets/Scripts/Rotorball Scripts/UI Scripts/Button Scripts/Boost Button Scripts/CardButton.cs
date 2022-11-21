using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    using Boosts;

    /// <summary>
    /// Handles the functionality and display behind a boost card.
    /// </summary>
    public class CardButton : UIButton
    {
        [SerializeField] private Boost boost;
        [Space]
        [SerializeField] private CanvasGroup group;
        [Space]
        [SerializeField] private SVGImage boostArrow;
        [Space]
        [SerializeField] private Text level;
        [SerializeField] private Text description;
        [SerializeField] private Text quantity;

        private BoostManager manager;

        public Boost Boost { get => boost; }

        public void Enable()
        {
            group.alpha = 1;
            group.interactable = true;
        }

        public void Disable()
        {
            group.alpha = 0.5f;
            group.interactable = false;
        }

        protected override void Init()
        {
            manager = FindObjectOfType<BoostManager>();

            level.text = "Lvl. " + boost.GetLevel().ToString();
            description.text = boost.Description;
            quantity.text = boost.GetStock().ToString() + " Available";
        }

        protected override void OnClicked()
        {
            manager.Apply(boost);
            int stock = boost.GetStock();
            quantity.text = stock.ToString() + " Available";

            boostArrow.sprite = boost.Icon;

            if (stock < 1)
            {
                Disable();
            }
        }
    }
}
