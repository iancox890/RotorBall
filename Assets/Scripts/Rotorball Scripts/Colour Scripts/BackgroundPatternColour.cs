using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.Colours
{
    /// <summary>
    /// Sets the background colour for the scene.
    /// </summary>
    public class BackgroundPatternColour : MonoBehaviour
    {
        [SerializeField] private Material backgroundMat;

        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
            ColourScheme.OnSchemeChange += OnEnable;
        }

        private void OnEnable() => image.color = backgroundMat.color;
        private void OnDisable() => ColourScheme.OnSchemeChange -= OnEnable;
    }
}
