using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace PsychedelicGames.RotorBall.LevelManagement
{
    using UI;
    public class Leaderboard : MonoBehaviour
    {
        [System.Serializable] public struct Score { public string name; public int score; }
        [System.Serializable] struct Result { public Score[] scores; }

        private static Leaderboard _instance;
        public static Leaderboard Instance
        {
            get {
                if (_instance == null)
                {
                    _instance = new Leaderboard();
                }
                return _instance; }
        }

        public const string BaseURL        = "http://86.163.60.68:8080/rotorball/";
        public const string AddScoreURL    = BaseURL+"add-score";
        public const string GetScoresURL   = BaseURL+"get-scores";

        [SerializeField] private GameObject _scoreList;
        [SerializeField] private GameObject _scorePrefab;
        // private LeaderboardScore[] _scores;

        void Start()
        {
            // string json = $"{{ {GetCurrentLevelAsJson()} }}";
            // StartCoroutine(GetScoresRequest(GetScoresURL, json));
            UpdateScoreboard();
        }

        void UpdateScoreboard()
        {
            for (int i=0; i<_scoreList.transform.childCount; i++)
            {
                Destroy(_scoreList.transform.GetChild(i).gameObject);
            }
            string json = $"{{ {GetCurrentLevelAsJson()} }}";
            StartCoroutine(GetScoresRequest(GetScoresURL, json));
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                _AddScore(new Score{name="is",score=666});
            }
        }

        string GetCurrentLevelAsJson()
        {
            string tier = "",level = "";
            if (LevelData.Current)
            {
                bool beforeHyphen = true;
                foreach (char c in LevelData.Current.Number)
                {
                    if (beforeHyphen)
                    {
                        if (c == '-') { beforeHyphen = false; continue; }
                        tier += c;
                    } else
                    {
                        level += c;
                    }
                }
            } else { tier = "1"; level = "2"; }
            return $" \"tier\": {tier}, \"level\": {level} ";
        }

        public static void AddScore(Score score)
        {
            Instance.AddScore($"{{ \"name\": {score.name}, \"score\": {score.score}, {Instance.GetCurrentLevelAsJson()} }}");
        }
        public static void AddScore(Score score,string tier, string level)
        {
            Instance.AddScore($"{{ \"name\": {score.name}, \"score\": {score.score}, \"tier\": {tier}, \"level\": {level} }}");
        }
        // public void AddScore(Score score, string tier, string level)
        // {

        // }
        // public void AddScore(Score score,int tier, int level)
        // {

        // }
        void _AddScore(Score score)
        {
            AddScore($"{{ \"name\": \"{score.name}\", \"score\": {score.score}, {Instance.GetCurrentLevelAsJson()} }}");
        }
        void AddScore(string json)
        {
            print(json);
            StartCoroutine(AddScoreRequest(AddScoreURL,json));
        }

        IEnumerator AddScoreRequest(string url, string json)
        {
            var request = new UnityWebRequest(url, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            //Send the request then wait here until it returns
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log("Error While Sending: " + request.error);
            }
            else
            {
                print(request.downloadHandler.text);
                UpdateScoreboard();
            }
        }

        IEnumerator GetScoresRequest(string url, string json)
        {
            var request = new UnityWebRequest(url, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            //Send the request then wait here until it returns
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log("Error While Sending: " + request.error);
            }
            else
            {
                Debug.Log("Received: " + request.downloadHandler.text);
                string scoreText = $"{{ \"scores\": {request.downloadHandler.text} }}";
                
                Score[] scores = JsonUtility.FromJson<Result>(scoreText).scores;
                RectTransform pt = _scoreList.GetComponent<RectTransform>();
                foreach (Score score in scores)
                {
                    GameObject scoreUI = Instantiate(_scorePrefab);
                    scoreUI.GetComponent<LeaderboardScore>().SetScore(score);
                    RectTransform t = scoreUI.GetComponent<RectTransform>();
                    // t.localPosition = new Vector3(Screen.width/2f,(i++)*48f,0f);
                    // t.sizeDelta = new Vector2(Screen.width*0.8f,32f);
                    t.SetParent(pt);
                    t.SetAsLastSibling();
                    t.localScale = Vector3.one;
                }
            }
        }
    }
}