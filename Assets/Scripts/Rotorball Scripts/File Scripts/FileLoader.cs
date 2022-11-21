using UnityEngine;
using System.Collections.Generic;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.Files
{
    /// <summary>
    /// Loads the game files when appropriate.
    /// </summary>
    public  class FileLoader : MonoBehaviour
    {
        [SerializeField] private LevelData[] levelData;
        private static bool isLoaded;

        private void Awake()
        {
            if (!isLoaded) { Load(); }
        }

        private void Load()
        {
            int length = levelData.Length;
            int sum = 0;

            for (int i = 0; i < length; i++)
            {
                ref var temp = ref levelData[i];
                if (temp)
                {
                    //Get the level file and initialise it is isn't already.
                    var file = FileUtility.GetFile<LevelFile>(temp.Scene.SceneName);
                    temp.File = file;

                    sum++;
                }
            }
            isLoaded = true;

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            print("Loaded (" + sum + ") file(s) out of (" + length + ") level data objects.");
#endif
        }
    }
}
