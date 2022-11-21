using UnityEngine;
using UnityEditor;

namespace PsychedelicGames.RotorBall.Colours
{
    /// <summary>
    /// Contains Material data for a particular scheme.
    /// </summary>
    [CreateAssetMenu(fileName = "Scheme", menuName = "Psychedelic Games/Colours/Colour Scheme")]
    public class ColourScheme : ScriptableObject
    {
        [SerializeField] private ColourMaterial background;
        [SerializeField] private ColourMaterial backgroundPatternOne;
        [SerializeField] private ColourMaterial backgroundPatternTwo;
        [SerializeField] private ColourMaterial gradient;
        [SerializeField] private ColourMaterial UIGradient;
        [SerializeField] private ColourMaterial backgroundBricksOne;
        [SerializeField] private ColourMaterial backgroundBricksTwo;
        [SerializeField] private ColourMaterial backgroundBricksThree;
        [SerializeField] private ColourMaterial UIMainOne;
        [SerializeField] private ColourMaterial UIMainTwo;
        [SerializeField] private ColourMaterial UIMainThree;
        [SerializeField] private ColourMaterial UIIconOne;
        [SerializeField] private ColourMaterial UIIconTwo;
        [SerializeField] private ColourMaterial UIIconThree;
        [SerializeField] private ColourMaterial UITextOne;
        [SerializeField] private ColourMaterial UITextTwo;
        [SerializeField] private ColourMaterial UITextThree;
        [SerializeField] private ColourMaterial brickStandard;
        [SerializeField] private ColourMaterial brickDurable;
        [SerializeField] private ColourMaterial brickHazard;
        [SerializeField] private ColourMaterial brickBlocker;
        [SerializeField] private ColourMaterial brickPowerupInner;
        [SerializeField] private ColourMaterial brickPowerupOuter;
        [SerializeField] private ColourMaterial brickBonus;
        [SerializeField] private ColourMaterial ball;
        [SerializeField] private ColourMaterial ballExplosion;
        [SerializeField] private ColourMaterial ballTrail;
        [SerializeField] private ColourMaterial highlightedDot;
        [SerializeField] private ColourMaterial slingshot;

        public static event System.Action OnSchemeChange;

        public void Apply()
        {
            background.AssignColour();
            Camera.main.backgroundColor = background.ObjectMat.color;
            backgroundPatternOne.AssignColour();
            backgroundPatternTwo.AssignColour();

            gradient.AssignColour();
            UIGradient.AssignColour();

            backgroundBricksOne.AssignColour();
            backgroundBricksTwo.AssignColour();
            backgroundBricksThree.AssignColour();

            UIMainOne.AssignColour();
            UIMainTwo.AssignColour();

            UIMainThree.AssignColour();
            UIIconOne.AssignColour();
            UIIconTwo.AssignColour();
            UIIconThree.AssignColour();

            UITextOne.AssignColour();
            UITextTwo.AssignColour();
            UITextThree.AssignColour();

            brickStandard.AssignColour();
            brickDurable.AssignColour();
            brickHazard.AssignColour();
            brickBlocker.AssignColour();
            brickPowerupInner.AssignColour();
            brickPowerupOuter.AssignColour();
            brickBonus.AssignColour();

            ball.AssignColour();
            ballExplosion.AssignColour();
            ballTrail.AssignColour();

            highlightedDot.AssignColour();
            slingshot.AssignColour();

            OnSchemeChange?.Invoke();
        }
    }
}
