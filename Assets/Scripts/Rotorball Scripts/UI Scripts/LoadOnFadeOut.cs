using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Loads a scene triggered by a menu button when the background is faded.
    /// </summary>
    public  class LoadOnFadeOut : MonoBehaviour
    {
        public GameScene Scene { get; set; }
        public void OnFadeOut() => SceneLoader.Load(Scene);
    }
}
