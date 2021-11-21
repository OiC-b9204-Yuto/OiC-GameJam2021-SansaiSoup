using AbductionCar.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler
{
    [SerializeField] private AudioClip clickSound;
    [SerializeField, Range(0.0f, 1.0f)] private float volumeScale = 1.0f;
    public UnityEvent onClick;

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (IsInteractable() == false)
        {
            return;
        }

        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        if (clickSound)
        {
            AudioManager.Instance.SE.PlayOneShot(clickSound, volumeScale);
        }
        onClick.Invoke();
    }
    public virtual void OnSubmit(BaseEventData eventData)
    {
        if (IsInteractable() == false)
        {
            return;
        }

        if (clickSound)
        {
            AudioManager.Instance.SE.PlayOneShot(clickSound);
        }
        DoStateTransition(SelectionState.Pressed, true);
        DoStateTransition(SelectionState.Normal, false);
        onClick.Invoke();
    }
}
