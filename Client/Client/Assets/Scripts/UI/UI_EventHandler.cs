using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Client
{
    /// <summary> 매개변수 없는 이벤트 핸들러 </summary>
    public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler
    {
        public Action<PointerEventData> OnClickHandler = null;
        public Action<PointerEventData> OnDragHandler = null;
        public Action<PointerEventData> OnDragEndHandler = null;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickHandler?.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnDragHandler?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnDragEndHandler?.Invoke(eventData);
        }
    }
}