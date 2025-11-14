using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class CCButton : MonoBehaviour, IPointerClickHandler
{
    [Serializable]
    public class LeftButtonClickedEvent : UnityEvent { }

    [FormerlySerializedAs("onLeftClick"),SerializeField]
    private LeftButtonClickedEvent m_OnLeftClick = new LeftButtonClickedEvent();

    [Serializable]
    public class RightButtonClickedEvent : UnityEvent { }

    [FormerlySerializedAs("onRightClick"), SerializeField]
    private RightButtonClickedEvent m_OnRightClick = new RightButtonClickedEvent();
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                m_OnLeftClick.Invoke();
                //左クリックの時の処理
                break;
            case PointerEventData.InputButton.Right:
                m_OnRightClick.Invoke();
                //右クロックの時の処理
                break;
            case PointerEventData.InputButton.Middle:
                //ホイールクリックの時のはない
                break;
            default:
                break;
        }
    }
}
