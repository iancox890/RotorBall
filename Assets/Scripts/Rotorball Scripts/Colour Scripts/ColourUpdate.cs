using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.Colours
{
    /// <summary>
    /// Updates an objects colour when a scheme is changed.
    /// </summary>
    public class ColourUpdate : MonoBehaviour
    {
        private void OnEnable()
        {
            ColourScheme.OnSchemeChange += UpdateColour;
        }

        private void OnDisable()
        {
            ColourScheme.OnSchemeChange -= UpdateColour;
        }

        private void UpdateColour()
        {
            gameObject.Deactivate();
            gameObject.Activate();
        }
    }
}
