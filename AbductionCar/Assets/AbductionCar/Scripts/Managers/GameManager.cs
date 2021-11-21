using AbductionCar.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                AudioManager.Instance.SetVolume(AudioManager.AudioGroup.CarEngine, isPause ? -80.0f : -7.5f);
            }
        }
        private const float InitTime = 120.0f;
        private float time = InitTime;
        private int score = 0;
        private int localHighScore = 0;

        private const string localHighScoreSaveFileName = "highscore.data";

        [SerializeField] AudioClip gameBGM;

        private float countDown = 3.0f;
        [SerializeField]private Text countDownText;

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
            AudioManager.Instance.BGM.loop = true;
            AudioManager.Instance.BGM.clip = gameBGM;
            AudioManager.Instance.BGM.Play();
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
                if(countDown > 0)
                {
                    countDown -= Time.unscaledDeltaTime;
                    countDownText.text = Math.Ceiling(countDown).ToString();
                }
                else
                {
                    countDownText.text = "START!";
                    GameStart();
                }
            }
            else
            {
                if (countDownText.color.a >= 0)
                {
                    var color = countDownText.color;
                    color.a -= 0.05f;
                    countDownText.color = color;
                }

                time -= Time.deltaTime;
                if (time <= 0)
                {
                    IsEnd = true;
                }
            }
        }

        public void GameStart()
        {
            if (!IsEnd)
            {
                IsStart = true;
            }
        }

        public float GetTime() { return time; }

        public int GetScore() { return score; }

        public void AddScore(int score)
        {
            if (score >= 0)
                this.score += score;
        }

        public int GetHighScore() { return localHighScore; }

        public void CheckLocalHighScore(int score)
        {
            if (localHighScore < score)
            {
                localHighScore = score;
                FileManager.Save(localHighScoreSaveFileName, localHighScore.ToString());
            }
        }
    }
}