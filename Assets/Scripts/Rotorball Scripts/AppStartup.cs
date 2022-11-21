using UnityEngine;
// using UnityEngine.Monetization;
using UnityEngine.Advertisements;

namespace PsychedelicGames.RotorBall
{
    using Colours;
    using Files;
    using Objectives.Achievments;
    using LevelManagement;

    /// <summary>
    /// Run startup code.
    /// </summary>
    public class AppStartup : MonoBehaviour
    {
        [Header("Adverts")]
        [SerializeField] string gameID;
        [SerializeField] bool testMode = true;
        [Header("Achievements")]
        [SerializeField] private bool _printAchievements = true;
        [SerializeField] private bool printStats = true;
        [SerializeField] private TierData[] tiers;
        [Header("Colour Schemes")]
        [SerializeField] public ColourScheme[] schemes;

        private bool tmp;

        private static AppStartup instance = null;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);

                // Monetization.Initialize(gameID, testMode);                
                Advertisement.Initialize(gameID, testMode);
                Advertisement.debugMode = true;

                Application.targetFrameRate = 60;
                AudioListener.volume = PlayerPrefs.HasKey("AudioVolume") ? PlayerPrefs.GetFloat("AudioVolume") : 1;

                if (!PlayerPrefs.HasKey("Tiers"))
                {
                    PlayerPrefs.SetInt("Tiers", 0);
                }

                LoadScheme();

                AchievementManager.CheckAchievements(StatisticsFile.File,printStats);
                AchievementManager.CheckAchievements(tiers,printStats);

                // foreach (Achievement a in AchievementManager.AchievementData.GetAll()) {
                //     print($"{a.name} Unlocked: { a.Achieved ");
                // }
                if (_printAchievements) { AchievementManager.PrintAll(); }

            } else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void LoadScheme()
        {
            string current = PlayerFile.GetFile().CurrentItems[(int)PlayerFile.Items.ColourScheme];
            int length = schemes.Length;

            for (int i = 0; i < length; i++)
            {
                ColourScheme scheme = schemes[i];
                if (scheme.name.Equals(current))
                {
                    scheme.Apply();
                }
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                tmp = !tmp;
                Time.timeScale = tmp ? 1f : 0.2f;
            }
        }
    }
}
