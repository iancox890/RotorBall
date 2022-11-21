using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    using LevelManagement;
    public class DisableIfNoLevelsCompleted : MonoBehaviour
    {
        [SerializeField] private TierData[] tierData;
        private void Start()
        {
            Button button = GetComponent<Button>();
            gameObject.Deactivate();
            foreach (TierData tier in tierData)
            {
                if (tier.TotalLevelsCompleted > 0)
                {
                    gameObject.Activate();
                    break;
                }
            }
        }
    }
}
