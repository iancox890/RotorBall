using UnityEngine;
using System.Text;

namespace PsychedelicGames
{
    /// <summary>
    /// Extension class for numeric data types.
    /// </summary>
    public static class NumericExtensions
    {
        private const int SecondsInMinute = 60;

        private static StringBuilder timeStrBuilder;

        static NumericExtensions()
        {
            timeStrBuilder = new StringBuilder(9, 9);
        }

        /// <summary>
        /// Converts seconds to the time format 00:00.000
        /// </summary>
        public static string ToFormattedTime(this float seconds)
        {
            if (seconds == float.MaxValue) { seconds = 0; }
            int roundedMinutes = (int)((seconds / SecondsInMinute) % SecondsInMinute);
            float roundedSeconds = seconds % SecondsInMinute;

            timeStrBuilder.Clear();

            timeStrBuilder.AppendFormat("{0:00}", roundedMinutes);
            timeStrBuilder.Append(":");
            timeStrBuilder.AppendFormat("{0:00.000}", roundedSeconds);

            return timeStrBuilder.ToString();
        }

        public static string FormatValue(this float value)
        {
            if (value > 100)
            {
                return value.ToString("#,##");
            }
            else
            {
                return value.ToString();
            }
        }

        public static string FormatValue(this int value)
        {
            if (value > 100)
            {
                return value.ToString("#,##");
            }
            else
            {
                return value.ToString();
            }
        }

        public static string FormatValue(this long value)
        {
            if (value > 100)
            {
                return value.ToString("#,##");
            }
            else
            {
                return value.ToString();
            }
        }
    }
}