using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace PsychedelicGames.RotorBall.EditorScripts
{
    /// <summary>
    /// Finds all missing components within a given selection.
    /// </summary>
    public class FindMissingComponents
    {
        [MenuItem("Psychedelic Games/Tools/Find Missing Components")]
        private static void Find()
        {
            var gameObjects = Selection.objects;
            var selected = new List<Object>();
            foreach (GameObject gameObject in gameObjects)
            {
                foreach (Component component in gameObject.GetComponents<Component>())
                {
                    if (!component)
                    {
                        selected.Add(gameObject);
                        break;
                    }
                }
            }
            Selection.objects = selected.ToArray();
        }
    }
}
