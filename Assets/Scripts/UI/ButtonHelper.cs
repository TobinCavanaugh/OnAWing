using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(BetterButton))]
    public class ButtonHelper : MonoBehaviour
    {
        public BetterButton button;

        [SerializeReference] public List<Helper> pointerEnterEvent = new();

        [SerializeReference] public List<Helper> clickEvent = new();

        public bool doLogging = false;

        //[SerializeReference]
        //public List<Helper> pointerExitEvent = new();

        private void Reset()
        {
            button = GetComponent<BetterButton>();
        }

        private void Start()
        {
            button = GetComponent<BetterButton>();
            button.OnPointerEnterEvent += PointerEnter;
            button.OnPointerExitEvent += PointerExit;
            button.OnClickEvent += OnClick;
        }

        public void OnClick()
        {
            if (doLogging)
                Debug.Log("Click");

            try
            {
                clickEvent?.ForEach(x => x.Activate());
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public void PointerEnter()
        {
            if (doLogging)
                Debug.Log("Enter");

            try
            {
                pointerEnterEvent?.ForEach(x => x.Activate());
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public void PointerExit()
        {
            if (doLogging)
                Debug.Log("Exit");

            try
            {
                pointerEnterEvent?.ForEach(x => x.Deactivate());
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}

