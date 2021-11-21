using AbductionCar.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbductionCar.Managers
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public bool IsEnd { get; private set; }
        public bool IsStart { get; private set; }
        public bool isPause;
        public bool IsPause {
            get { return isPause; }
            set
            {
                isPause = value;
                Time.timeScale = isPause ? 0.0f : 1.0f;
                AudioManager.Instance.SetVolume(AudioManager.AudioGroup.CarEngine, isPause ? -80.0f : -5.0f);
            }
        }

        private const float InitTime = 120.0f;

        private float time = InitTime;
        public float GetTime() { return time; }

        private int score = 0;

        public int GetScore() { return score; }
        public void AddScore(int score)
        {
            if(score >= 0)
            this.score += score;
        }


        private int localHighScore = 0;
        public int GetHighScore() { return localHighScore; }
        private const string localHighScoreSaveFileName = "highscore.data";
        public void CheckLocalHighScore(int score)
        {
            if (localHighScore < score)
            {
                localHighScore = score;
                FileManager.Save(localHighScoreSaveFileName, localHighScore.ToString());
            }
        }

        


        void Start()
        {
            IsEnd = false;
            IsStart = false;
            IsPause = false;
            time = InitTime;
            score = 0;
            bool result;
            string data = FileManager.Load(localHighScoreSaveFileName, out result);
            int parseResult;
            if(!int.TryParse(data, out parseResult) || !result)
            {
                parseResult = 0;
                FileManager.Save(localHighScoreSaveFileName, "0");
            }
            localHighScore = parseResult;
        }

        void Update()
        {
            if (SceneTransitionManager.Instance.IsLoadScene)
            {
                return;
            }

            if (IsEnd)
            {
                return;
            }

            if(IsPause)
            {
                return;
            }

            if (!IsStart)
            {
                GameStart();
            }

            time -= Time.deltaTime;
            if(time <= 0)
            {
                IsEnd = true;
            }
        }

        public void GameStart()
        {
            if (!IsEnd)
            {
                IsStart = true;
            }
        }


    }
}