using AbductionCar.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AbductionCar.UI
{
    [RequireComponent(typeof(Text))]
    public class ScoreText : MonoBehaviour
    {
        Text text;
        private void Awake()
        {
            text = GetComponent<Text>();
        }

        void Start()
        {
            text.text = "スコア  " + GameManager.Instance.GetScore().ToString("D5");
        }

        void Update()
        {
            text.text = "スコア  " + GameManager.Instance.GetScore().ToString("D5");
        }
    }
}
