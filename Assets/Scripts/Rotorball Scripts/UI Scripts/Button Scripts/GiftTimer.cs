using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    public class GiftTimer : MonoBehaviour
    {
        [SerializeField] GiftReset gift;
        [SerializeField] Text timeRemainingText;
        [SerializeField] private string unlockedMessage;
        private CountUp _counter;

        private void Start() {
            _counter = GetComponent<CountUp>();    
        }

        // Update is called once per frame
        void Update()
        {
            if (_counter==null || !_counter.Counting)
            {
                if (gift.IsUnlocked())
                {
                    timeRemainingText.text = unlockedMessage;
                } else
                {
                    timeRemainingText.text = gift.TimeRemainingFormatted();
                }
            }
        }
    }
}