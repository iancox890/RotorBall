using UnityEngine;
using PsychedelicGames.RotorBall.Colours;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Sets the current colour scheme to the scheme attached to the button.
    /// </summary>
    public class ColourSchemeButton : UIButton
    {
        [SerializeField] private ColourScheme scheme;

        protected override void OnClicked() { scheme.Apply(); }
    }
}