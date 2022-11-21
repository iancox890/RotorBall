using UnityEngine;

namespace PsychedelicGames.RotorBall.Colours
{
    /// <summary>
    /// Sets the background colour for the scene.
    /// </summary>
    [ExecuteAlways]
    public class BackgroundColour : MonoBehaviour
    {
        [SerializeField] private Material backgroundMat;
        private void Start() => Camera.main.backgroundColor = backgroundMat.color;
    }
}
