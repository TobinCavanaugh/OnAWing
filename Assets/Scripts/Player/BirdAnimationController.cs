using DG.Tweening;
using MilkShake;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class BirdAnimationController : MonoBehaviour
    {
        public Material material;
        public bool wingInPosition = false;

        [ColorUsage(true, true)] 
        public Color defaultColor;
        
        [ColorUsage(true, true)] 
        public Color chargedColor;

        public float colorLerpSpeed = 1.0f;

        public ShakePreset idleShakeProfile;

        public Slider chargeSlider;
        public float chargeSliderUpTime = .4f;
        public float chargeSliderDownTime = .4f;
        public float chargeSliderRelaxTime = .92f;

        public AnimationCurve chargeUp;
        public AnimationCurve chargeDown;

        public CameraFOVPunch cameraFOVPunch;

        public AudioSource wingsUp;

        public void SliderRelax()
        {
            var v = chargeSlider.DOValue(10, chargeSliderRelaxTime, false);
            v.SetEase(chargeDown);
        }

        public void SliderUp()
        {
            wingsUp.Play();
            var v = chargeSlider.DOValue(10, chargeSliderUpTime, false);
            v.SetEase(chargeUp);
            
            cameraFOVPunch.CameraPunch(0, true);
            //cameraFOVPunch.Punch(cameraFOVPunch.presets[0], cameraFOVPunch.presets[1]);
        }

        public void SliderDown()
        {
            var v = chargeSlider.DOValue(0, chargeSliderDownTime, false);
            v.SetEase(chargeDown);
            cameraFOVPunch.Return();
        }
        
        public void WingUp()
        {
            wingInPosition = true;
        }

        public void WingDown()
        {
            wingInPosition = false;
        }

        [SerializeField, ReadOnly]
        private bool isCharging = false;

        [SerializeField, ReadOnly]
        private Color targetColor;
        
        private void Update()
        {
            var t = defaultColor;
            if (isCharging)
            {
                t = chargedColor;
            }
            else
            {
                t = defaultColor;
            }

            targetColor = Color.Lerp(targetColor, t, Time.deltaTime * colorLerpSpeed);
            material.SetColor("_BaseColor", targetColor);
            material.SetColor("_EmissionColor", targetColor);
            
            //material.SetColor(Property,
            //         Color.Lerp(material.GetColor(Property), lerpColor, Time.deltaTime * colorLerpSpeed));
        }

        public void IdleShake()
        {
            Shaker.GlobalShakers[0].Shake(idleShakeProfile);
        }

        public void StartCharge()
        {
            isCharging = true;
        }

        public void EndCharge()
        {
            isCharging = false;
        }
    }
}
