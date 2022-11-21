using UnityEngine;

namespace PsychedelicGames.RotorBall
{
    using Files;

    /// <summary>
    /// Randomises a given item for the ball.
    /// </summary>
    public class ItemRandomiser : MonoBehaviour
    {
        [SerializeField] private StoreItem.Type type;
        [SerializeField] private PlayerItems items;

        private void Awake()
        {
            PlayerFile file = PlayerFile.GetFile();
            string randomItem;
            GameObject randomGO = null;

            switch (type)
            {
                case StoreItem.Type.Style:
                    randomItem = file.StyleItems[Random.Range(0, file.StyleItems.Count)];
                    randomGO = items.GetStyle(randomItem);

                    break;

                case StoreItem.Type.Trail:
                    randomItem = file.TrailItems[Random.Range(0, file.TrailItems.Count)];
                    randomGO = items.GetTrail(randomItem);

                    break;

                case StoreItem.Type.Explosion:
                    randomItem = file.ExplosionItems[Random.Range(0, file.ExplosionItems.Count)];
                    randomGO = items.GetExplosion(randomItem);

                    break;
            }

            Instantiate(randomGO, transform);
        }
    }
}
