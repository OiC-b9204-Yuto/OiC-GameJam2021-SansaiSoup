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
        [SerializeField] private Text highScoreText;

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
                    scoreText.text = GameManager.Instance.GetScore().ToString();
                    //ゲームマネージャー内完結でいい気がしてきた
                    GameManager.Instance.CheckLocalHighScore(GameManager.Instance.GetScore());
                    highScoreText.text = GameManager.Instance.GetHighScore().ToString();
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
                else
                {
                    inputField.interactable = false;
                    sendButton.interactable = false;
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

        public void ReturnToTitle()
        {
            SceneTransitionManager.Instance.LoadSceneStart("TitleScene");
        }
    }
}
