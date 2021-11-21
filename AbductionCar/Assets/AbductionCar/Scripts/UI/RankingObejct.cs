using AbductionCar.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AbductionCar.UI {
    public class RankingObejct : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Text rankText;
        [SerializeField] private Text nameText;
        [SerializeField] private Text scoreText;

        public void SetValue(RankingData data)
        {
            rankText.text = data.rankNum.ToString();
            nameText.text = data.name;
            scoreText.text = data.score.ToString();
        }

        public void ValueClear()
        {
            rankText.text = "";
            nameText.text = "";
            scoreText.text = "";
        }

        public void ChangeBackgroundColor(Color color)
        {
            background.color = color;
        }
    }
}