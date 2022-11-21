using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Sets a text component to that of the version number.
    /// </summary>
    public class VersionNumberText : MonoBehaviour
    {
        private void Start() => GetComponent<Text>().text = "V" + Application.version;
    }
}