using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbductionCar.Managers;

namespace AbductionCar.UI
{
    public class TitleMenu : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {

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