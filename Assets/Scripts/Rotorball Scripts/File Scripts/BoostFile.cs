using UnityEngine;

namespace PsychedelicGames.RotorBall.Files
{
    /// <summary>
    /// File for our boosts info.
    /// </summary>
    [System.Serializable]
    public struct BoostFile : IFileData
    {
        private int stock;
        private int level;
        private bool tutorialComplete;

        public int Stock { get => stock; set => stock = value; }
        public int Level { get => level; set => level = value; }
        public bool TutorialComplete { get => tutorialComplete; set => tutorialComplete = value; }
    }
}
