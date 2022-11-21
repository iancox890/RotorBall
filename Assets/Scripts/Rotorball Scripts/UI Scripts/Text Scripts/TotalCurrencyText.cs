using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    using Files;

    /// <summary>
    /// Displays the total currency.
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class TotalCurrencyText : MonoBehaviour
    {
        private Text currencyText;
        private CountUp countUp;

        public void UpdateDisplay() => currencyText.text = PlayerFile.GetFile().RotorPoints.FormatValue();
        public void UpdateDisplay(int price) {
            if (countUp != null) {
                countUp.Count(PlayerFile.GetFile().RotorPoints + price,PlayerFile.GetFile().RotorPoints);
            } else {
                UpdateDisplay();
            }
        } 

        private void Awake() 
        {
            currencyText = GetComponent<Text>();
            countUp = GetComponent<CountUp>();
        }

        private void OnEnable() => UpdateDisplay();
    }
}
