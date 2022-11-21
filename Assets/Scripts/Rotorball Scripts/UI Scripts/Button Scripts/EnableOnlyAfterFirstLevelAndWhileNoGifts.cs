using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    using Files;
    using LevelManagement;
    public class EnableOnlyAfterFirstLevelAndWhileNoGifts : MonoBehaviour
    {
        [SerializeField] private bool _invert;
        [SerializeField] private bool _atLeastOne;
        [SerializeField] private TierData[] _tierData;
        private void Start()
        {
            gameObject.SetActive(_invert);
            if (StatisticsFile.File.giftsOpened <= 0)
            {
                int levelsCompleted = 0;
                foreach (TierData tier in _tierData)
                {
                    if (tier.TotalLevelsCompleted > 0)
                    {
                        levelsCompleted += tier.TotalLevelsCompleted;
                    }
                }
                if (levelsCompleted > 0 && (_atLeastOne || (!_atLeastOne && levelsCompleted == 1)))
                {
                    // If never opened a gift and completed exactly least one level
                    gameObject.SetActive(!_invert);
                    return;
                }
            }
        }
    }
}
