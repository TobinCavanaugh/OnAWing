using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SliderHelper : MonoBehaviour
    {
        public string beforeFloat;
        public string afterFloat;
        public TextMeshProUGUI tmp;
        public Slider slider;
        public AudioSource clickSource;


        private void Awake()
        {
            slider.onValueChanged.AddListener(x => SetValue());

        }

        private void Start()
        {
            slider = GetComponent<Slider>();

            SetValue();
        }

        private float oldVal;
        public void SetValue()
        {
            float val = RoundedValue(slider.value);
            slider.value = val;

            if (val - oldVal != 0)
            {
                clickSource.Play();
            }
            
            oldVal = val;
            tmp.text = beforeFloat + val + afterFloat;
        }

        private float RoundedValue(float val)
        {
            return (float)Math.Round(val, 1);
        }
    }

}