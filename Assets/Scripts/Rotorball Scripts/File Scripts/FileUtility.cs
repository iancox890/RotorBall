using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace PsychedelicGames.RotorBall.Files
{
    /// <summary>
    /// Utility class for saving and getting game files.
    /// </summary>
    public static class FileUtility
    {
        private static readonly BinaryFormatter formatter;
        private static readonly string fileDirectory;

        static FileUtility()
        {
            formatter = new BinaryFormatter();
            fileDirectory = Directory.CreateDirectory(Application.persistentDataPath + "/GameFiles").FullName;
        }

        /// <summary>
        /// Overwrites data of type T to a file with fileName.
        /// </summary>
        public static void OverwriteFile<T>(T data, string fileName) where T : IFileData, new()
        {
            FileInfo info = new FileInfo(GetPath(fileName));

            using (FileStream file = info.Create())
            {
                formatter.Serialize(file, data);
            }
        }

        /// <summary>
        /// Gets a file of type object and a name of fileName.
        /// If no file of this type/name is found, one is created.
        /// </summary>
        public static T GetFile<T>(string fileName) where T : IFileData, new()
        {
            FileInfo info = new FileInfo(GetPath(fileName));

            if (!info.Exists || info.Length == 0)
            {
                OverwriteFile(new T(), fileName);
            }

            using (FileStream file = info.Open(FileMode.Open))
            {
                return (T)formatter.Deserialize(file);
            }
        }

        /// <summary>
        /// Clears all the files within the Game Files directory. 
        /// </summary>
        public static void ClearFiles(bool delete)
        {
            PlayerFile.ClearFile();
            
            string[] files = Directory.GetFiles(fileDirectory);

            int length = files.Length;
            int sum = 0;

            for (int i = 0; i < length; i++)
            {
                if (!delete)
                {
                    File.WriteAllText(files[i], string.Empty);
                }
                else
                {
                    File.Delete(files[i]);
                }
                sum++;
            }
#if UNITY_EDITOR
            if (!delete) 
            { 
                Debug.Log("Cleared (" + sum + ") file(s) out of (" + length + ") file(s) in directory."); }
            else 
            { 
                Debug.Log("Deleted (" + sum + ") file(s) out of (" + length + ") file(s) in directory.");
            }
#endif
        }

        public static string GetPath(string fileName) => fileDirectory + "/" + fileName + ".dat";
    }
}
