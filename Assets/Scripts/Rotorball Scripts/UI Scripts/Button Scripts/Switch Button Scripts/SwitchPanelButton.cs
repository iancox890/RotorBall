using UnityEngine;
using PsychedelicGames.RotorBall.Animations;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Switches a given panel.
    /// </summary>
    public class SwitchPanelButton : UIButton
    {
        [SerializeField] private PanelDirection direction;
        [SerializeField] private float clickDelta;

        private enum PanelDirection { Next, Previous }

        [SerializeField] private InfoPanelAnimatorGeneric panel;
        private float time;
        private bool IsClickable
        {
            get
            {
                if (Time.unscaledTime - time > clickDelta) { return true; }
                return false;
            }
        }

        //protected override void Init() => panel = FindObjectOfType<InfoPanelAnimatorGeneric>();

        protected override void OnClicked()
        {
            if (IsClickable)
            {
                time = Time.unscaledTime;
                if (direction == PanelDirection.Next) { panel.Next(); }
                else { panel.Prev(); }
            }
        }
    }
}
