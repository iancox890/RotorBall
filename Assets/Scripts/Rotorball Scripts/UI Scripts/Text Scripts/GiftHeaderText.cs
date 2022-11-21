using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    [RequireComponent(typeof(UnityEngine.UI.Text))]
    public class GiftHeaderText : MonoBehaviour
    {
        [SerializeField] private GiftReset gift;
        [SerializeField] private string waitingText = "NEXT GIFT IN...";
        [SerializeField] private string readyText = "NEXT GIFT...";
        private UnityEngine.UI.Text text;
        private bool unlocked;

        private void OnEnable()
        {
            text = GetComponent<UnityEngine.UI.Text>();
            unlocked = !gift.IsUnlocked();
            
        }

        // Update is called once per frame
        void Update()
        {
            if (unlocked != gift.IsUnlocked())
            {
                unlocked = !unlocked;
                text.text = unlocked ? readyText : waitingText;
            }
        }
    }
}
