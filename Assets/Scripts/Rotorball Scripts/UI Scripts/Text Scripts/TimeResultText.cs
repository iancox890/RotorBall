using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Displays the resulting time after a level is completed.
    /// </summary>
    public class TimeResultText : MonoBehaviour
    {
        private void Awake() => GetComponent<Text>().text = FindObjectOfType<LevelManagement.LevelManager>().FinishTime.ToFormattedTime();
    }
}
