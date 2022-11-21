using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Selects a new store item.
    /// </summary>
    public class SelectStoreItemButton : UIButton
    {
        [SerializeField] private InspectedItemDisplay display;
        private StoreItem itemToSelect;

        private void Start()
        {
            display = display ?? FindObjectOfType<InspectedItemDisplay>();
        }

        protected override void Init() => itemToSelect = GetComponent<ItemState>().Item;
        protected override void OnClicked()
        {
            StoreItem.InspectedItem = itemToSelect;
            display.UpdateDisplay();
        }
    }
}
