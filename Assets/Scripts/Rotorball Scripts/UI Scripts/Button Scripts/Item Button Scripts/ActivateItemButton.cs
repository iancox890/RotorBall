using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    using Files;

    /// <summary>
    /// Activates a store item.
    /// </summary>
    public class ActivateItemButton : UIButton
    {
        [SerializeField] private InspectedItemDisplay itemDisplay;
        [SerializeField] private ItemListDisplay itemListDisplay;

        protected override void OnClicked()
        {
            StoreItem item = StoreItem.InspectedItem;

            PlayerFile file = PlayerFile.GetFile();

            file.CurrentItems[(int)item.ItemType] = item.ItemKey;
            if (item.ItemType == StoreItem.Type.ColourScheme)
            {
                item.ItemScheme.Apply();
            }

            PlayerFile.SaveFile();

            itemDisplay.UpdateDisplay();
            itemListDisplay.UpdateDisplay();
        }
    }
}
