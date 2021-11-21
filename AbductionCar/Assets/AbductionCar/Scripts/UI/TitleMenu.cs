using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbductionCar.Managers;

namespace AbductionCar.UI
{
    public class TitleMenu : MonoBehaviour
    {
        //エスケープキーで閉じるため
        [SerializeField] private OptionsMenu optionsMenu;
        [SerializeField] private RankingMenu rankingMenu;

        [SerializeField] AudioClip titleBGM;

        void Start()
        {
            AudioManager.Instance.BGM.loop = true;
            AudioManager.Instance.BGM.clip = titleBGM;
            AudioManager.Instance.BGM.Play();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (optionsMenu.IsActive())
                {
                    optionsMenu.Undo();
                    optionsMenu.Disable();
                }
                else if (rankingMenu.IsActive())
                {
                    rankingMenu.Disable();
                }
            }
        }

        public void GamePlay()
        {
            SceneTransitionManager.Instance.LoadSceneStart("GameScene");
        }

        public void GameQuit()
        {
            SceneTransitionManager.Instance.GameQuitStart();
        }
    }
}