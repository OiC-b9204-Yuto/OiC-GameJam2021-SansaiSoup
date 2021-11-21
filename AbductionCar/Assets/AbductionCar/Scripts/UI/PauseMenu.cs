using AbductionCar.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AbductionCar.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private Selectable enableSelectedObejct;

        //エスケープキーで閉じるため
        [SerializeField] private OptionsMenu optionsMenu;
        //選択カーソル
        [SerializeField] private SelectedCursor selectedCursor;


        private void Start()
        {
            selectedCursor.CursorEnabled(false);
        }

        void Update()
        {
            //終わっている場合はポーズさせない
            if (GameManager.Instance.IsEnd)
            {
                return;
            }

            //始まる前はポーズさせない
            if (!GameManager.Instance.IsStart)
            {
                return;
            }


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (optionsMenu.IsActive())
                {
                    optionsMenu.Undo();
                    optionsMenu.Disable();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Pause()
        {
            if (GameManager.Instance.IsPause)
            {
                GameManager.Instance.IsPause = false;
                pausePanel.SetActive(false);
                selectedCursor.CursorEnabled(false);
            }
            else
            {
                GameManager.Instance.IsPause = true;
                pausePanel.SetActive(true);
                enableSelectedObejct.Select();
                selectedCursor.CursorEnabled(true);
            }
        }

        public void ReturnToTitle()
        {
            SceneTransitionManager.Instance.LoadSceneStart("TitleScene");
        }
    }
}