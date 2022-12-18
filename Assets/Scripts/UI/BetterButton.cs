using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class BetterButton : Button
    {
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            OnPointerEnterEvent?.Invoke();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            OnPointerExitEvent?.Invoke();
        }

        public void OnMouseOver()
        {
            OnPointerStayEvent?.Invoke();
            Debug.Log("Hover");
        }

        protected override void Awake()
        {
            base.Awake();
            onClick.AddListener(() => OnClickEvent?.Invoke());
        }


        public event Action OnPointerEnterEvent;

        public event Action OnPointerExitEvent;

        public event Action OnClickEvent;

        public event Action OnPointerStayEvent;
    }
}