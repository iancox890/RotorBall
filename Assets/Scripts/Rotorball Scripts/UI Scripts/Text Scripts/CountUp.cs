using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    [RequireComponent(typeof(Text))]
    public class CountUp : MonoBehaviour
    {
        [SerializeField] private float countTime;
        [SerializeField] private bool _isTime;
        [SerializeField] private string _prefix;
        [SerializeField] private string _postFix;
        [System.NonSerialized]
        public bool Counting;
        private Text text;
        private long target;
        private long initial;
        private float difference;
        private float startedCounting;

        private void Awake()
        {
            text = GetComponent<Text>();
            Counting = false;
        }

        public void Count(Vector2Int fromTo,float countTime)
        {
            this.countTime = countTime;
            Count(fromTo);
        }

        public void Count(Vector2Int fromTo)
        {
            Count((long)fromTo[0],(long)fromTo[1]);
        }

        public void Count(long from, long to, float countTime)
        {
            this.countTime = countTime;
            Count(from,to);
        }

        public void Count(long from, long to)
        {
            initial = from;
            target  = to;
            difference = (float)(target - initial);
            startedCounting = Time.time;
            Counting = true;
            StartCoroutine(Step());
        }

        private void OnDisable() {
            StopAllCoroutines();
        }

        private IEnumerator Step()
        {
            while (Counting)
            {
                float timeElapsed = Time.time - startedCounting;
                if (timeElapsed >= countTime)
                {
                    SetText(_isTime ? GiftReset.FormatTime(target) : target.FormatValue());
                    Counting = false;
                    // break;
                    yield break;
                }
                else
                {
                    long current = (initial + (long)(difference * (timeElapsed / countTime)));
                    SetText(_isTime ? GiftReset.FormatTime(current) : current.FormatValue());
                    yield return null;
                }
            }
            // StopCoroutine("Step");
            yield break;
        }

        private void SetText(string newText) {
            text.text = _prefix + newText + _postFix;
        }
    }
}

