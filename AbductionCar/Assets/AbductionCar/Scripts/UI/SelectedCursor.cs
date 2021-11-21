using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AbductionCar.UI
{
    public class SelectedCursor : MonoBehaviour
    {
        [SerializeField] private Image cursorImage;
        [SerializeField] private string ignoreTag;
        private EventSystem eventSystem;
        private GameObject selectedObject;

        private RectTransform _rectTransform;

        private void Awake()
        {
            eventSystem = (EventSystem)FindObjectOfType(typeof(EventSystem));
            _rectTransform = GetComponent<RectTransform>();
        }

        void Start()
        {
            selectedObject = eventSystem.firstSelectedGameObject;
            CursorMove();
        }

        void Update()
        {
            if (!eventSystem.currentSelectedGameObject)
            {
                //クリックで選択状態が外れた場合戻れるように
                if (Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0 ||
                    Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0)
                {
                    selectedObject.GetComponent<Selectable>().Select();
                }
            }
            else
            {
                if (selectedObject != eventSystem.currentSelectedGameObject)
                {
                    selectedObject = eventSystem.currentSelectedGameObject;
                    if (string.IsNullOrEmpty(ignoreTag) == false && selectedObject.tag == ignoreTag)
                    {
                        cursorImage.enabled = false;
                    }
                    else
                    {
                        cursorImage.enabled = true;
                        CursorMove();
                    }
                }
            }
        }

        void CursorMove()
       {
            RectTransform rect = selectedObject.GetComponent<RectTransform>();
            _rectTransform.position = rect.position;
            _rectTransform.sizeDelta = new Vector2(rect.sizeDelta.x + 5, rect.sizeDelta.y + 5);
        }

        public void CursorEnabled(bool b)
        {
            cursorImage.enabled = b;
        }

        public bool GetEnabled()
        {
            return cursorImage.enabled;
        }
    }
}
