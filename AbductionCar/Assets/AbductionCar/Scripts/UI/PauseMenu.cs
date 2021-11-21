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

        //�G�X�P�[�v�L�[�ŕ��邽��
        [SerializeField] private OptionsMenu optionsMenu;
        //�I���J�[�\��
        [SerializeField] private SelectedCursor selectedCursor;


        private void Start()
        {
            selectedCursor.CursorEnabled(false);
        }

        void Update()
        {
            //�I����Ă���ꍇ�̓|�[�Y�����Ȃ�
            if (GameManager.Instance.IsEnd)
            {
                return;
            }

            //�n�܂�O�̓|�[�Y�����Ȃ�
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