using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.Animations
{
    /// <summary>
    /// Handles the level details information panels.
    /// </summary>
    public class InfoPanelNumberedAnimation : InfoPanelAnimatorGeneric
    {
        [SerializeField] protected Text numberText;

        override protected void Start()
        {
            base.Start();
            UpdatePageNumber();
        }

        override public void Next()
        {
            NextPage();
            UpdatePageNumber();
        }

        override public void Prev()
        {
            PreviousPage();
            UpdatePageNumber();
        }

        protected virtual void UpdatePageNumber()
        {
            numberText.text = (index + 1).ToString() + " / " + GetLength().ToString();
        }
    }
}
