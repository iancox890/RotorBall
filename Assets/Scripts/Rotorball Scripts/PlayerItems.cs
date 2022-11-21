using UnityEngine;

namespace PsychedelicGames.RotorBall
{
    /// <summary>
    /// Holds all the player items.
    /// </summary>
    [CreateAssetMenu(fileName = "Player Items.asset", menuName = "Psychedelic Games/Player Items")]
    public class PlayerItems : ScriptableObject
    {
        [SerializeField] private GameObject[] styles;
        [SerializeField] private GameObject[] trails;
        [SerializeField] private GameObject[] explosions;

        public GameObject GetStyle(string key) => GetKey(styles, key);
        public GameObject GetTrail(string key) => GetKey(trails, key);
        public GameObject GetExplosion(string key) => GetKey(explosions, key);

        private GameObject GetKey(GameObject[] array, string key)
        {
            int length = array.Length;
            for (int i = 0; i < length; i++)
            {
                GameObject temp = array[i];
                if (temp.name.Equals(key))
                {
                    return temp;
                }
            }
            return null;
        }
    }
}
