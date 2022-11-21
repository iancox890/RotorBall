using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    using Files;
    public class ColourSchemeBackgroundLevelSwitcher : MonoBehaviour
    {
        [SerializeField] Animator[] backgrounds;
        private int hell = 0;

        private int index;

        private void Awake()
        {
            index = -1;
            if (backgrounds.Length == 0)
            {
                backgrounds = GetComponentsInChildren<Animator>();
            }
        }

        public void HellOn()
        {
            backgrounds[hell].transform.localEulerAngles = new Vector3(0f, 0f, 180f);
        }

        public void HellOff()
        {
            backgrounds[hell].transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        private void OnEnable()
        {
            if (backgrounds.Length == 0)
            {
                print("No backgrounds set");
                gameObject.Deactivate();
            }
            else
            {
                int prev = index;
                do
                {
                    index = Random.Range(0, backgrounds.Length);
                } while (index == prev);

                for (int i = 0; i < backgrounds.Length; i++)
                {
                    if (i != index && backgrounds[i].enabled)
                    {
                        backgrounds[i].ResetTrigger("Enter");
                        backgrounds[i].Play("Exit", -1, 1f);
                    }
                }

                print($"Activating background {index}, {backgrounds[index].gameObject.name}");

                // backgrounds[index].gameObject.Activate();
                backgrounds[index].gameObject.SetActive(true);
                backgrounds[index].enabled = true;
                backgrounds[index].SetTrigger("Enter");

                print($"AVASD: {backgrounds[index].gameObject.activeInHierarchy} and {backgrounds[index].gameObject.activeSelf}");
            }
        }

        public void Next()
        {
            int next = index + 1;
            next = next > backgrounds.Length - 1 ? 0 : next;
            MoveFromTo(index, next);
            index = next;
        }

        private void MoveFromTo(int from, int to)
        {
            backgrounds[from].ResetTrigger("Enter");
            backgrounds[from].SetTrigger("Exit");
            StartCoroutine(DisableAfterAnimation(from));
            backgrounds[to].gameObject.Activate();
            backgrounds[to].enabled = true;
            backgrounds[to].ResetTrigger("Exit");
            backgrounds[to].SetTrigger("Enter");
        }

        private IEnumerator DisableAfterAnimation(int index)
        {
            while (!backgrounds[index].GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Exited")) {
                yield return new WaitForSeconds(0.1f);
            }
            backgrounds[index].gameObject.Deactivate();
            yield break;
        }
    }
}