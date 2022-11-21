using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Sets the text to a given piece of store item data.
    /// </summary>
    public class StoreItemText : MonoBehaviour
    {
        [SerializeField] private StoreItem defaultItem;
        [SerializeField] private InfoCode infoCode;

        private Text infoText;
        private StoreItem inspectedItem;

        private enum InfoCode { Name, Description }

        private System.Action setText;

        private void Awake()
        {
            infoText = GetComponent<Text>();
            switch (infoCode)
            {
                case InfoCode.Name:
                    setText = () => infoText.text = StoreItem.InspectedItem.ItemName;
                    break;
                case InfoCode.Description:
                    setText = () => infoText.text = StoreItem.InspectedItem.ItemDescription;
                    break;
            }
        }

        private void OnEnable()
        {
            StoreItem.OnInspectedItemChanged += setText;
            StoreItem.InspectedItem = defaultItem;
        }

        private void OnDisable()
        {
            StoreItem.OnInspectedItemChanged -= setText;
        }
    }
}
