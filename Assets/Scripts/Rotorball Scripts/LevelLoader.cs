using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall
{
    /// <summary>
    /// Loads a level via async.
    /// </summary>
    public  class LevelLoader : MonoBehaviour
    {
        [SerializeField] private float minWaitTime;

        private Animator loadAnimator;
        private AsyncOperation loadingOperation;
        private int levelLoaded = Animator.StringToHash("Level Loaded");
        private string levelPath;

        private void Start()
        {
            loadAnimator = GetComponent<Animator>();
            if (LevelData.Current)
            {
                levelPath = LevelData.Current.Scene.ScenePath;
                StartCoroutine(Load());
            }
            else { Debug.Log("No LevelData found!"); }
        }

        public IEnumerator Load()
        {
            loadingOperation = SceneManager.LoadSceneAsync(levelPath);
            loadingOperation.allowSceneActivation = false;

            yield return new WaitForSeconds(minWaitTime);

            while (!loadingOperation.isDone)
            {
                if (loadingOperation.progress == 0.9f) { loadAnimator.SetTrigger(levelLoaded); }
                yield return null;
            }
        }

        public void ActivateScene() => loadingOperation.allowSceneActivation = true;
    }
}
