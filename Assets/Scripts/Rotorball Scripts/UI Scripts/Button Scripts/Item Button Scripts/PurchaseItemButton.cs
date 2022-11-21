
using UnityEngine;
using UnityEngine.EventSystems;

namespace PsychedelicGames.RotorBall.UI
{
    using Files;

    /// <summary>
    /// Purchases a store item.
    /// </summary>
    public class PurchaseItemButton : UIButton
    {
        [SerializeField] private GameObject confirmationDisplay;
        [SerializeField] private InspectedItemDisplay itemDisplay;
        [SerializeField] private ItemListDisplay itemListDisplay;
        [SerializeField] private TotalCurrencyText currencyText;

        protected override void OnClicked()
        {
            PlayerFile playerFile = PlayerFile.GetFile();
            StatisticsFile statisticsFile = StatisticsFile.File;
            StoreItem item = StoreItem.InspectedItem;
            string key = item.ItemKey;

            playerFile.RotorPoints -= item.ItemPrice;
            statisticsFile.RPSpent += item.ItemPrice;

            StatisticsFile.File = statisticsFile;

            switch (item.ItemType)
            {
                case StoreItem.Type.Style:
                    playerFile.StyleItems.Add(key);
                    break;
                case StoreItem.Type.Trail:
                    playerFile.TrailItems.Add(key);
                    break;
                case StoreItem.Type.Explosion:
                    playerFile.ExplosionItems.Add(key);
                    break;
                case StoreItem.Type.ColourScheme:
                    playerFile.ColourSchemes.Add(key);
                    item.ItemScheme.Apply();
                    break;
            }
            playerFile.CurrentItems[(int)item.ItemType] = key;

            PlayerFile.SaveFile();

            confirmationDisplay.Deactivate();

            itemDisplay.UpdateDisplay();
            itemListDisplay.UpdateDisplay();
            currencyText.UpdateDisplay(item.ItemPrice);
        }
    }
}
