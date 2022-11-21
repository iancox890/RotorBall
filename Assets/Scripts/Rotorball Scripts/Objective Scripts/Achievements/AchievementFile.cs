// //TODO: Remove this file


//using UnityEngine;
//using System.Collections.Generic;

//namespace PsychedelicGames.RotorBall.Objectives.Achievements
//{
//    using LevelManagement;
//    using Boosts;

//    /// <summary>
//    /// Holds data related to tutorials.
//    /// </summary>
//    [System.Serializable]
//    public class AchievementFile : IFileData
//    {
//        // Meta variables
//        public const string FileName = "Tutorial";
//        private static TutorialFile file;
//        public static TutorialFile File
//        {
//            get
//            {
//                if (file == null)
//                { file = FileUtility.GetFile<TutorialFile>(FileName); }
//                return file;
//            }
//            set { file = value; }
//        }
//        private static TutorialData data;
//        public static TutorialData Data
//        {
//            get
//            {
//                if (data == null)
//                { data = Resources.Load<TutorialData>("Data/TutorialData"); }
//                return data;
//            }
//            set { data = value; }
//        }

//        // File data
//        private Dictionary<Boost,bool> completed;

//        // Constructor
//        public TutorialFile()
//        {
//            completed = new Dictionary<Boost, bool>();
//            foreach (Boost boost in data.boosts)
//            {
//                completed.Add(boost,false);
//            }
//        }


//        // Functions
//        public static void SaveFile()
//        {
//            if (File != null)
//            {
//                FileUtility.OverwriteFile(File, FileName);
//            }
//        }

//        public static void ClearFile()
//        {
//            File = new TutorialFile();
//            SaveFile();
//        }

//        private static void Validate()
//        {
//            if (Data.tutorials.Length != File.completed.Count)
//            {
//                ClearFile();
//            }
//        }

//        public static void CompleteBoostTutorial(Boost boost)
//        {
//            Validate();
//            File.completed[boost] = true;
//            SaveFile();
//        }

//        public static bool IsBoostTutorialComplete(Boost boost)
//        {
//            Validate();
//            return File.completed[boost];
//        }

//        //public static void CompleteLevel(LevelData data)
//        //{
//        //    Validate();
//        //    for (int i = 0; i < Data.tutorials.Length; i++)
//        //    {
//        //        TutorialLevel tutorial = Data.tutorials[i];
//        //        if (tutorial.level.Equals(data))
//        //        {
//        //            File.completed[i] = true;
//        //        }
//        //    }
//        //    SaveFile();
//        //}

//        //public static bool IsTutorialComplete(LevelData data)
//        //{
//        //    Validate();
//        //    for (int i = 0; i < Data.tutorials.Length; i++)
//        //    {
//        //        TutorialLevel tutorial = Data.tutorials[i];
//        //        if (tutorial.level.Equals(data))
//        //        {
//        //            return File.completed[i];
//        //        }
//        //    }
//        //    return false;
//        //}
//    }
//}
