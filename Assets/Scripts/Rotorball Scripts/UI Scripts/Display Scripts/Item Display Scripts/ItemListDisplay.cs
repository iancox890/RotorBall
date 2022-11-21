using UnityEngine;
using System.Collections.Generic;

namespace PsychedelicGames.RotorBall.UI
{
    using Files;

    /// <summary>
    /// Sets the states for the items list.
    /// </summary>
    public class ItemListDisplay : MonoBehaviour
    {
        [SerializeField] private ItemState[] items;
        [SerializeField] private StoreItem.Type type;

        public void UpdateDisplay()
        {
            PlayerFile file = PlayerFile.GetFile();
            int index = (int)type;

            List<string> purchasedArray = new List<string>();
            string current = file.CurrentItems[index];

            switch (type)
            {
                case StoreItem.Type.Style:
                    purchasedArray = file.StyleItems;
                    break;
                case StoreItem.Type.Trail:
                    purchasedArray = file.TrailItems;
                    break;
                case StoreItem.Type.Explosion:
                    purchasedArray = file.ExplosionItems;
                    break;
                case StoreItem.Type.ColourScheme:
                    purchasedArray = file.ColourSchemes;
                    break;
            }

            int length = items.Length;

            for (int i = 0; i < length; i++)
            {
                ItemState temp = items[i];
                string key = temp.Item.ItemKey;

                if (key.Equals(current))
                {
                    temp.SetAsActive();
                }
                else if (purchasedArray.Contains(key) || key.Contains("Random"))
                {
                    temp.SetAsPurchased();
                }
                else
                {
                    temp.SetAsUnpurchased();
                }
            }
        }

        private void OnEnable() => UpdateDisplay();
    }
}
