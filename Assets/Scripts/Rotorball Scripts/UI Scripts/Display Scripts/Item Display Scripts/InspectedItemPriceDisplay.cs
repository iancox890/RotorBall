using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Displays the price for the currently inspected store item.
    /// </summary>
    public class InspectedItemPriceDisplay : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float affordableAlpha;
        [SerializeField] private float unaffordableAlpha;

        private Text priceText;

        private void Awake() => priceText = GetComponent<Text>();

        private void OnEnable()
        {
            StoreItem inspected = StoreItem.InspectedItem;
            priceText.text = inspected.ItemPrice.FormatValue();

            if (inspected.IsAffordable)
            {
                canvasGroup.alpha = affordableAlpha;
                canvasGroup.interactable = true;
            }
            else
            {
                canvasGroup.alpha = unaffordableAlpha;
                canvasGroup.interactable = false;
            }
        }
    }
}
