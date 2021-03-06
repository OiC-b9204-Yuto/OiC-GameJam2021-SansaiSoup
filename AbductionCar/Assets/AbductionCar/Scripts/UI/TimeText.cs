using AbductionCar.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace AbductionCar.UI
{
    [RequireComponent(typeof(Text))]
    public class TimeText : MonoBehaviour
    {
        Text text;

        private void Awake()
        {
            text = GetComponent<Text>();
        }

        private void Start()
        {
            text.text = "残り時間  " + GameManager.Instance.GetTime().ToString("F1").PadLeft(5) + "秒";
        }

        void Update()
        {
            text.text = "残り時間  " + GameManager.Instance.GetTime().ToString("F1").PadLeft(5) + "秒";
        }
    }
}