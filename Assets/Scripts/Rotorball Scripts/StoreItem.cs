using UnityEngine;

namespace PsychedelicGames.RotorBall
{
    using Files;
    using Colours;
    using UI;

    /// <summary>
    /// Represents a customistation item.
    /// </summary>
    [CreateAssetMenu(fileName = "Store Item.asset", menuName = "Psychedelic Games/Store Item")]
    public class StoreItem : ScriptableObject
    {
        [SerializeField] private ColourScheme itemScheme;
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private Sprite itemSprite;
        [SerializeField] private GameObject itemPreview;
        [SerializeField] private int itemPrice;
        [SerializeField] private string itemName;
        [SerializeField] private string itemDescription;
        [SerializeField] private Type itemType;

        public enum Type { Style = 0, Trail = 1, Explosion = 2, ColourScheme = 3 }

        public ColourScheme ItemScheme { get => itemScheme; }
        public GameObject ItemPrefab { get => itemPrefab; }
        public Sprite ItemSprite { get => itemSprite; }
        public GameObject ItemPreview { get => itemPreview; }
        public int ItemPrice { get => itemPrice; }
        public string ItemDescription { get => itemDescription; }
        public string ItemName { get => itemName; }
        public string ItemKey
        {
            get
            {
                if (ItemType != Type.ColourScheme)
                {
                    return itemPrefab.name;
                }
                else
                {
                    return itemScheme.name;
                }
            }
        }
        public Type ItemType { get => itemType; }

        public bool IsAffordable
        {
            get
            {
                return PlayerFile.GetFile().RotorPoints >= itemPrice;
            }
        }

        private static StoreItem inspectedItem;
        public static StoreItem InspectedItem
        {
            get => inspectedItem;
            set
            {
                inspectedItem = value;
                if (inspectedItem.ItemType == Type.ColourScheme)
                {
                    if (inspectedItem.ItemName == "HELL")
                    {
                        FindObjectOfType<ColourSchemeBackgroundLevelSwitcher>().HellOn();
                    }
                    else
                    {
                        FindObjectOfType<ColourSchemeBackgroundLevelSwitcher>().HellOff();
                    }
                    inspectedItem.ItemScheme.Apply();
                }
                OnInspectedItemChanged?.Invoke();
            }
        }

        public static event System.Action OnInspectedItemChanged;
    }
}
