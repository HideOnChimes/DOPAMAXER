using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class DoubleTapImage : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent onDoubleTap;

    [SerializeField] private float doubleTapThreshold = 0.3f;

    private float lastClickTime = -1f;
    public void OnPointerClick(PointerEventData eventData)
    {
        float currentTime = Time.time;
        if (currentTime - lastClickTime < doubleTapThreshold)
        {
            onDoubleTap.Invoke();
            lastClickTime = -1f;
        }
        lastClickTime = currentTime;
    }

}
