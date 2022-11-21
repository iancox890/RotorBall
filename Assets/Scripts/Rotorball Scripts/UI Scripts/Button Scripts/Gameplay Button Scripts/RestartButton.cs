using UnityEngine;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Restarts the game on click.
    /// </summary>
    public class RestartButton : UIButton
    {
        protected override void OnClicked() => SceneLoader.Reload();
    }
}