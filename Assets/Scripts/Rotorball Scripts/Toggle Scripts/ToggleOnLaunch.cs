using UnityEngine;
using PsychedelicGames.RotorBall.Gameplay;

namespace PsychedelicGames.RotorBall
{
    /// <summary>
    /// Toggles a game object when the ball is put in and out of play.
    /// </summary>
    public  class ToggleOnLaunch : MonoBehaviour
    {
        private Slingshot slingshot;

        private void Start()
        {
            slingshot = FindObjectOfType<Slingshot>();

            if (slingshot)
            {
                slingshot.OnLaunched += gameObject.Deactivate;
                slingshot.OnReloaded += gameObject.Activate;
            }
        }

        private void OnDestroy()
        {
            if (slingshot)
            {
                slingshot.OnLaunched -= gameObject.Deactivate;
                slingshot.OnReloaded -= gameObject.Activate;
            }
        }
    }
}
