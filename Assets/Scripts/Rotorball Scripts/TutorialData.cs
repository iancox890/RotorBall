using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PsychedelicGames.RotorBall
{
    using LevelManagement;
    using Boosts;

    [System.Serializable]
    public class TutorialLevel
    {
        public LevelData level;
    }

    [CreateAssetMenu(fileName = "TutorialData", menuName = "Psychedelic Games/Tutorial Data")]
    public class TutorialData : ScriptableObject
    {
        [SerializeField] public TutorialLevel[] tutorials;
        [SerializeField] public Boost[] boosts;

        private IEnumerable<TutorialLevel> GetTutorials(LevelData level) { return tutorials.Where((tutorial) => tutorial.level == level); }

        public bool HasTutorial(LevelData level)
        {
            return GetTutorials(level).Count() > 0;
        }

        public TutorialLevel GetTutorial(LevelData level)
        {
            IEnumerable<TutorialLevel> matches = GetTutorials(level);
            if (matches.Count() > 0)
            {
                return matches.ElementAt(0);
            }
            else
            {
                return null;
            }
        }

        public static void CompleteBoostTutorial(Boost boost)
        {
            boost.CompleteTutorial();
        }

        public Boost[] RequiredBoostTutorials()
        {
            return boosts.Where((x) => x.TutorialNeeded()).ToArray();
        }

        public bool BoostTutorialsRequired()
        {
            return RequiredBoostTutorials().Length > 0;
        }
    }
}
