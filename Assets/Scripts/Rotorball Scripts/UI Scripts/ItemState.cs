using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Sets the state for a store item.
    /// </summary>
    public class ItemState : MonoBehaviour
    {
        [SerializeField] private StoreItem item;
        [SerializeField] private GameObject tick;
        [SerializeField] private CanvasGroup group;

        public StoreItem Item { get => item; }

        public void SetAsActive()
        {
            tick.Activate();
            group.alpha = 1;
        }

        public void SetAsPurchased()
        {
            tick.Deactivate();
            group.alpha = 1;
        }

        public void SetAsUnpurchased()
        {
            group.alpha = 0.5f;
        }
    }
}
