using UnityEngine;
using System.Collections.Generic;

namespace PsychedelicGames.RotorBall.UI
{
    using Files;

    /// <summary>
    /// Displays the current info for the currently inspected store item.
    /// </summary>
    public class InspectedItemDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject pricingDisplay;
        [SerializeField] private GameObject confirmationDisplay;
        [SerializeField] private GameObject activeDisplay;
        [SerializeField] private GameObject activateDisplay;

        private GameObject display;

        public void UpdateDisplay()
        {
            PlayerFile file = PlayerFile.GetFile();
            StoreItem item = StoreItem.InspectedItem;

            bool isActive = false;
            bool isPurchased = false;

            string[] array = file.CurrentItems;
            string key = item.ItemKey;

            int length = array.Length;

            for (int i = 0; i < length; i++)
            {
                if (array[i].Equals(key))
                {
                    isActive = true;
                    isPurchased = true;

                    break;
                }
            }

            if (!isPurchased)
            {
                List<string> list = new List<string>();

                switch (item.ItemType)
                {
                    case StoreItem.Type.Style:
                        list = file.StyleItems;
                        break;
                    case StoreItem.Type.Trail:
                        list = file.TrailItems;
                        break;
                    case StoreItem.Type.Explosion:
                        list = file.ExplosionItems;
                        break;
                    case StoreItem.Type.ColourScheme:
                        list = file.ColourSchemes;
                        break;
                }

                if (list.Contains(key) || key.Contains("Random"))
                {
                    isPurchased = true;
                }
                else
                {
                    isPurchased = false;
                }
            }

            GameObject previousDisplay = display;

            if (isActive)
            {
                display = activeDisplay;
            }
            else if (isPurchased)
            {
                display = activateDisplay;
            }
            else
            {
                display = pricingDisplay;
                confirmationDisplay.Deactivate();
            }

            previousDisplay.Deactivate();
            display.Activate();
        }

        private void OnEnable() => StoreItem.OnInspectedItemChanged += UpdateDisplay;
        private void OnDisable() => StoreItem.OnInspectedItemChanged -= UpdateDisplay;
    }
}
