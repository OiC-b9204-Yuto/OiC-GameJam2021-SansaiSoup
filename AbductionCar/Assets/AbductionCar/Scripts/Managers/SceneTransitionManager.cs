using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AbductionCar.Framework;
#if UNITY_EDITOR
using UnityEditor;
#endif   

namespace AbductionCar.Managers
{
    /// <summary>
    /// シーン管理用クラス
    /// </summary>
    public class SceneTransitionManager : SingletonMonoBehaviour<SceneTransitionManager>
    {
        [SerializeField] private Image fadeImage;
        private float fadeSpeed = 1;

        private bool isLoadScene = false;
        /// <summary>
        /// シーン読み込み中フラグ
        /// </summary>
        public bool IsLoadScene { get { return isLoadScene; } }

        override protected void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this.gameObject);
            Color color = fadeImage.color;
            color.a = 1;
            fadeImage.color = color;
        }

        private void Start()
        {
            StartCoroutine(FadeIn());
        }

        /// <summary>
        /// シーンを読み込み遷移する関数
        /// </summary>
        /// <param name="name">シーン名</param>
        public void LoadSceneStart(string name)
        {
            if (isLoadScene)
            {
                return;
            }
            isLoadScene = true;
            StartCoroutine(LoadScene(name));
        }

        /// <summary>
        /// シーンを読み込み遷移する関数
        /// </summary>
        /// <param name="index">シーン番号</param>
        public void LoadSceneStart(int index)
        {
            if (isLoadScene)
            {
                return;
            }
            isLoadScene = true;
            StartCoroutine(LoadScene(index));
        }

        private IEnumerator LoadScene(string name)
        {
            var scene = SceneManager.LoadSceneAsync(name);
            scene.allowSceneActivation = false;
            yield return FadeOut();
            scene.allowSceneActivation = true;
            yield return FadeIn();
            isLoadScene = false;
        }

        private IEnumerator LoadScene(int index)
        {
            var scene = SceneManager.LoadSceneAsync(name);
            scene.allowSceneActivation = false;
            yield return FadeOut();
            scene.allowSceneActivation = true;
            yield return FadeIn();
            isLoadScene = false;
        }

        private IEnumerator FadeIn()
        {
            Color color = fadeImage.color;
            while (color.a >= 0)
            {
                color.a -= fadeSpeed * Time.deltaTime;
                fadeImage.color = color;
                yield return null;
            }
        }

        private IEnumerator FadeOut()
        {
            Color color = fadeImage.color;
            while (color.a <= 1)
            {
                color.a += fadeSpeed * Time.deltaTime;
                fadeImage.color = color;
                yield return null;
            }
        }

        /// <summary>
        /// ゲームを終了する関数
        /// </summary>
        public void GameQuitStart()
        {
            if (isLoadScene)
            {
                return;
            }
            isLoadScene = true;
            StartCoroutine(GameQuit());
        }

        private IEnumerator GameQuit()
        {
            yield return FadeOut();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit ();
#endif
        }
    }
}