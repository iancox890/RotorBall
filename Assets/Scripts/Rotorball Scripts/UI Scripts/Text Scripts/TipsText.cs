using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Sets a text component to a random tip.
    /// </summary>
    public class TipsText : MonoBehaviour
    {
        [SerializeField] private Tips tips;
        [Space]
        [SerializeField] private Text header;
        [SerializeField] private Text description;

        private void Start()
        {
            Tip tip = tips.GetTip();

            header.text = tip.Header;
            description.text = tip.Description;
        }
    }
}
