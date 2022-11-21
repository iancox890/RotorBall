using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Displays the preview for the currently inspected item.
    /// </summary>
    public class InspectedItemPreviewDisplay : MonoBehaviour
    {
        [SerializeField] private Transform preview;

        private SVGImage previewImage;

        private void UpdateDisplay()
        {
            StoreItem item = StoreItem.InspectedItem;
            if (item.ItemType != StoreItem.Type.ColourScheme)
            {
                if (previewImage)
                {
                    previewImage.sprite = item.ItemSprite;
                }
                else
                {
                    GameObject itemPreview = item.ItemPreview;
                    int count = preview.childCount;

                    for (int i = 0; i < count; i++)
                    {
                        Destroy(preview.GetChild(i).gameObject);
                    }

                    Instantiate(itemPreview, preview);
                }
            }
        }

        private void Awake() => previewImage = preview.GetComponent<SVGImage>();
        private void OnEnable() => StoreItem.OnInspectedItemChanged += UpdateDisplay;
        private void OnDisable() => StoreItem.OnInspectedItemChanged -= UpdateDisplay;
    }
}
