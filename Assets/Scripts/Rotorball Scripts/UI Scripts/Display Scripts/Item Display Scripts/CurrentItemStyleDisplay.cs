using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    using Files;

    /// <summary>
    /// Displays the sprite selected for the currently active style.
    /// </summary>
    public class CurrentItemStyleDisplay : MonoBehaviour
    {
        [SerializeField] private StoreItem[] styles;
        [SerializeField] private new SpriteRenderer renderer;

        private int length;
        private string current;

        private void Awake() => length = styles.Length;

        private void OnEnable()
        {
            string name = PlayerFile.GetFile().CurrentItems[(int)PlayerFile.Items.Style];

            if (!name.Equals(current))
            {
                current = name;

                for (int i = 0; i < length; i++)
                {
                    StoreItem item = styles[i];
                    string prefabName = item.ItemPrefab.name;

                    if (prefabName.Equals(name))
                    {
                        renderer.sprite = item.ItemSprite;
                        renderer.material = item.ItemPrefab.GetComponent<SpriteRenderer>().sharedMaterial;
                    }
                }
            }
        }
    }
}
