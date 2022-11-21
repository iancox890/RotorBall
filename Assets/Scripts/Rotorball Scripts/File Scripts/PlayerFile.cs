using UnityEngine;
using System.Collections.Generic;

namespace PsychedelicGames.RotorBall.Files
{
    /// <summary>
    /// Holds data related to the player.
    /// </summary>
    [System.Serializable]
    public class PlayerFile : IFileData
    {
        public const string FileName = "Player";

        public static PlayerFile file;

        public static PlayerFile GetFile()
        {
            if (file == null)
            {
                file = FileUtility.GetFile<PlayerFile>(FileName);
            }
            return file;
        }

        public static void SaveFile()
        {
            if (file != null)
            {
                FileUtility.OverwriteFile(file, FileName);
            }
        }

        public static void ClearFile()
        {
            file = new PlayerFile();
            FileUtility.OverwriteFile(file, FileName);
        }

        // Time last gift was claimed

        public System.DateTime giftLastClaimed { get; set; }

        // Points

        private int rotorPoints = 0;
        public int RotorPoints { get => rotorPoints; set => rotorPoints = value; }

        private int experiencePoints = 0;
        public int ExperiencePoints { get => experiencePoints; set => experiencePoints = value; }

        private int experiencelevel = 0;
        public int ExperienceLevel { get => experiencelevel; set => experiencelevel = value; }

        // Items

        public enum Items { Style = 0, Trail = 1, Explosion = 2, ColourScheme = 3 }

        private string[] currentItems = new string[4] { "Default Style", "Default Trail", "Default Explosion", "Default Scheme" };
        public string[] CurrentItems { get => currentItems; set => currentItems = value; }

        private List<string> styleItems = new List<string>();
        public List<string> StyleItems { get => styleItems; set => styleItems = value; }

        private List<string> trailItems = new List<string>();
        public List<string> TrailItems { get => trailItems; set => trailItems = value; }

        private List<string> explosionItems = new List<string>();
        public List<string> ExplosionItems { get => explosionItems; set => explosionItems = value; }

        private List<string> colourSchemes = new List<string>();
        public List<string> ColourSchemes { get => colourSchemes; set => colourSchemes = value; }

        private bool _EULASigned = false;
        public bool EULASigned { get => _EULASigned; set => _EULASigned = value; }

        public PlayerFile()
        {
            giftLastClaimed = System.DateTime.Now;
            styleItems.Add(currentItems[((int)Items.Style)]);
            trailItems.Add(currentItems[((int)Items.Trail)]);
            explosionItems.Add(currentItems[((int)Items.Explosion)]);
            colourSchemes.Add(currentItems[((int)Items.ColourScheme)]);
        }
    }
}
