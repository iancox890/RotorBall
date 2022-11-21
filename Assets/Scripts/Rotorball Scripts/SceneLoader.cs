using UnityEngine;
using UnityEngine.SceneManagement;
using PsychedelicGames.RotorBall.Files;

namespace PsychedelicGames.RotorBall
{
    /// <summary>
    /// Allows for loading of scenes.
    /// </summary>
    public static class SceneLoader
    {
        /// <summary>
        /// Loads a given scene.
        /// </summary>
        /// <param name="scene">The game scene to load.</param>
        public static void Load(GameScene scene)
        {
            TimeUtility.Resume();
            SceneManager.LoadScene(scene.ScenePath);
        }

        /// <summary>
        /// Reloads the currently active scene.
        /// </summary>
        public static void Reload()
        {
            StatisticsFile statisticsFile = FileUtility.GetFile<StatisticsFile>("Statistics");
            statisticsFile.LevelsRestarted++;
            FileUtility.OverwriteFile(statisticsFile, "Statistics");

            TimeUtility.Resume();
            SceneManager.LoadScene(SceneManager.GetActiveScene().path);
        }
    }
}
