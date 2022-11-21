using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.Animations
{
    /// <summary>
    /// Handles the level details information panels.
    /// </summary>
    public  class InfoPanelAnimation : InfoPanelAnimatorGeneric
    {
        [SerializeField] private SVGImage[] circles;
        [Space]
        [SerializeField] private Material circleActive;
        [SerializeField] private Material circleInActive;

        override public void NextPage()
        {
            //Slide out the current panel
            GetCurrent().Play(slideOutLeft);
            circles[index].material = circleInActive;

            //If we're at the end of the array, start at the beginning.
            //If not, progress forward.
            if ((index + 1) > (GetLength() - 1)) { index = 0; }
            else { index++; }

            //Slide in the next panel.
            GetCurrent().Play(slideInRight);
            circles[index].material = circleActive;
        }

        override public void PreviousPage()
        {
            //Slide out the current panel
            GetCurrent().Play(slideOutRight);
            circles[index].material = circleInActive;

            //If we're at the beginning of the array, start at the end.
            //If not, progress backwards.
            if ((index - 1) < 0) { index = GetLength() - 1; }
            else { index--; }

            //Slide in the next panel.
            GetCurrent().Play(slideInLeft);
            circles[index].material = circleActive;
        }
    }
}
