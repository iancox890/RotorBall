using UnityEngine;

namespace PsychedelicGames
{
    /// <summary>
    /// Extensions for the GameObject class.
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Activates the GameObject.
        /// </summary>
        public static void Activate(this GameObject obj)
        {
            if (obj)
                obj.SetActive(true);
        }
        /// <summary>
        /// Deactivates the GameObject.
        /// </summary>
        public static void Deactivate(this GameObject obj)
        {
            if (obj)
                obj.SetActive(false);
        }
    }
}
