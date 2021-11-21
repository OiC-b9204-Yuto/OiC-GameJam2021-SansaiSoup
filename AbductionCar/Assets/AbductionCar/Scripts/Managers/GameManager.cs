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
        public bool IsPause { get; set; }

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

        void Start()
        {
            IsEnd = false;
            IsStart = false;
            IsPause = false;
            time = InitTime;
            score = 0;
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