using System.Collections;
using System.Linq;
using UnityEngine;

namespace PsychedelicGames.RotorBall.Audio
{
    /// <summary>
    /// Plays music in a given sequence.
    /// </summary>
    public class MusicSequencer : MonoBehaviour
    {
        [SerializeField] private Track[] tracks;
        [SerializeField] private bool randomise;

        private AudioSource source;

        private int index;
        private int length;

        private static MusicSequencer _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);

                source = GetComponent<AudioSource>();

                index = -1;
                length = tracks.Length;

                StartCoroutine("Play");
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private IEnumerator Play()
        {
            int[] tracksPlayed = new int[length];;
            int numTracksPlayed = length;

            while (true)
            {
                int newIndex;
                if (randomise)
                {
                    if (numTracksPlayed >= length)
                    {
                        for (int i=0; i<length; i++) { tracksPlayed[i] = -1; }
                        numTracksPlayed = 0;
                    }
                    newIndex = index;
                    do
                    {
                        newIndex = Random.Range(0, length);
                    }
                    while (tracksPlayed.Contains(newIndex));
                    tracksPlayed[numTracksPlayed++] = newIndex;
                }
                else
                {
                    newIndex = index < length - 1 ? index + 1 : 0;
                }
                index = newIndex;
                
                source.clip = tracks[index].Clip;
                source.Play();
                
                yield return new WaitForSeconds(source.clip.length);
            }
            // Invoke("Play", source.clip.length);
        }
    }
}
