using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PsychedelicGames.RotorBall
{

    using Files;
    using LevelManagement;
    using UI;

    public class MenuTimer : MonoBehaviour
    {

        private static MenuTimer instance = null;

        private LevelManager levelManager;
        private float timeElapsed;
        private bool saved;
        private bool paused;

        // Executed before Start
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(this);

            timeElapsed = 0f;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (levelManager == null)
            {
                levelManager = FindObjectOfType<LevelManager>();
                StatisticsDataDisplay statDisplay = FindObjectOfType<StatisticsDataDisplay>();
                if (statDisplay != null)
                {
                    statDisplay.UpdateTimers(timeElapsed);
                }
                // Could optimise by storing initial time and calculating difference when saving
                timeElapsed += Time.deltaTime;
                if (saved)
                {
                    saved = false;
                }
            } else
            {
                // Save when entering a level
                if (!saved)
                {
                    saved = true;
                    Save();
                    timeElapsed = 0f;
                }
                
                // Keep track of time spent paused
                if (timeElapsed != 0f && !(paused || levelManager.completed.activeInHierarchy || levelManager.failed.activeInHierarchy))
                {
                    Save();
                    timeElapsed = 0f;
                }
                if (paused || levelManager.completed.activeInHierarchy || levelManager.failed.activeInHierarchy)
                {
                    timeElapsed += Time.unscaledDeltaTime;
                }
            }
        }

        public void Pause()
        {
            paused = true;
        }

        public void Unpause()
        {
            paused = false;
        }

        private void Save()
        {
            StatisticsFile statisticsFile = StatisticsFile.File;
            statisticsFile.timePlayedMenu += timeElapsed;
            statisticsFile.timePlayedTotal += timeElapsed;
            StatisticsFile.File = statisticsFile;
        }
    }
}
