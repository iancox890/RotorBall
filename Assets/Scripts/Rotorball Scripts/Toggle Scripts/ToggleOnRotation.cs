using UnityEngine;
using PsychedelicGames.RotorBall.Gameplay;

namespace PsychedelicGames.RotorBall
{
    /// <summary>
    /// Toggles a collider when the rotation state is updated.
    /// </summary>
    public class ToggleOnRotation : MonoBehaviour
    {
        private OldRotor rotor;
        private new Collider2D collider;

        private void Awake()
        {
            rotor = FindObjectOfType<OldRotor>();
            collider = GetComponent<Collider2D>();
        }

        private void OnEnable()
        {
            //if (rotor) { rotor.OnRotationUpdated += Toggle; }
        }
        private void OnDisable()
        {
            //if (rotor) { rotor.OnRotationUpdated -= Toggle; }
        }

        private void Toggle(bool value) => collider.enabled = !value;
    }
}
