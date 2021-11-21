using AbductionCar.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AbductionCar.UI
{
    public class ResultMenu : MonoBehaviour
    {
        [SerializeField] GameObject ResultPanel;

        [SerializeField] private Selectable enableSelectedObejct;

        [SerializeField] private Text scoreText;

        [SerializeField] private InputField inputField;
        [SerializeField] private CustomButton sendButton;
        [SerializeField] private CustomButton rankingButton;

        private bool save = false;

        private void Start()
        {
            save = false;
        }

        void Update()
        {
            if(ResultPanel.activeSelf == false)
            {
                if (GameManager.Instance.IsEnd)
                {
                    enableSelectedObejct.Select();
                    ResultPanel.SetActive(true);
                }
            }else
            {

                if (!save)
                {
                    if (inputField.text == "")
                    {
                        sendButton.interactable = false;
                    }
                    else
                    {
                        sendButton.interactable = true;
                    }
                }
            }
        }

        public void SendScore()
        {
            save = RankingManager.Instance.SaveRanking(inputField.text, GameManager.Instance.GetScore());
            if(save)
            {
                inputField.interactable = false;
                sendButton.interactable = false;
                rankingButton.Select();
            }
        }
    }
}
