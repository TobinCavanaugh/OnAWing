using System;
using DG.Tweening;
using MilkShake;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerBody : MonoBehaviour
    {
        public SplineBasedBirdController splineBasedBirdController;
        public float timeBeforeReturn = 2f;
        public float lerpSpeed = 1f;
        public float boostMult = 15f;
        private float curTime = 0;
        public AnimationCurve boostCurve;
        public ShakePreset boostShake;

        private void Update()
        {
            curTime += Time.deltaTime;

            if (curTime >= timeBeforeReturn)
            {
                splineBasedBirdController.curOffset = Vector3.Lerp(splineBasedBirdController.curOffset, Vector3.zero,
                    Time.deltaTime * lerpSpeed);
            }
        }

        private Tweener _tweener;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out AirCurrent airCurrent))
            {
                curTime = 0;
                
                _tweener?.Kill();
                _tweener = DOVirtual.Vector3(splineBasedBirdController.curOffset,
                    airCurrent.GetBoostDirection() * boostMult,
                    timeBeforeReturn,
                    x =>
                    {
                        splineBasedBirdController.curOffset = x;
                    })
                    .SetEase(boostCurve);

                Shaker.GlobalShakers[0].Shake(boostShake);
            }
        }
    }
}