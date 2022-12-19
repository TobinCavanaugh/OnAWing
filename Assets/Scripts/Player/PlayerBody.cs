using System.Collections;
using DG.Tweening;
using Map;
using MilkShake;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerBody : MonoBehaviour
    {

        
        public SplineBasedBirdController splineBasedBirdController;

        #region AIR_CURRENT

        private const string AIR_CURRENT = "Air Current";
        
        [FoldoutGroup(AIR_CURRENT)]
        public float airCurrentBoostLengthTime = 2f;
        
        [FoldoutGroup(AIR_CURRENT)]
        public float airBoostLerpSpeed = 1f;
        
        [FoldoutGroup(AIR_CURRENT)]
        public float airCurrentBoostMult = 15f;
        private float _airBoostCurTime = 0;
        
        [FoldoutGroup(AIR_CURRENT)]
        public AnimationCurve boostCurve;
        
        [FoldoutGroup(AIR_CURRENT)]
        public ShakePreset boostShake;
        

        #endregion

        #region CRASHING

        private const string CRASHING = "Crashing";
        
        [FoldoutGroup(CRASHING)]
        public float crashedSpeedMult = .8f;
        
        [FoldoutGroup(CRASHING)]
        public float crashTimeTransitionTime = .5f;
        
        [FoldoutGroup(CRASHING)]
        public float crashedTimeLength = 3f;
        
        [FoldoutGroup(CRASHING)]
        public float crashedMoveLerpSpeed = 1.5f;
        
        [FoldoutGroup(CRASHING)]
        public float crashReturnTime = 2f;

        [FormerlySerializedAs("slowCurve")] [FoldoutGroup(CRASHING)]
        public AnimationCurve crashCurve = AnimationCurve.Linear(0,0, 1,1);

        [FoldoutGroup(CRASHING)]
        public AnimationCurve returnCurve = AnimationCurve.Linear(0,0, 1,1);
        

        #endregion

        public UnityEvent featherPickupUE;
        private void Update()
        {
            _airBoostCurTime += Time.deltaTime;

            if (_airBoostCurTime >= airCurrentBoostLengthTime)
            {
                splineBasedBirdController.curOffset = Vector3.Lerp(splineBasedBirdController.curOffset, Vector3.zero,
                    Time.deltaTime * airBoostLerpSpeed);
            }
        }

        private Tweener _tweener;
        
        private void OnTriggerEnter(Collider other)
        {
            //Handles entering air currents
            if (other.TryGetComponent(out AirCurrent airCurrent))
            {
                _airBoostCurTime = 0;
                
                _tweener?.Kill();
                _tweener = DOVirtual.Vector3(splineBasedBirdController.curOffset,
                    airCurrent.GetBoostDirection() * airCurrentBoostMult,
                    airCurrentBoostLengthTime,
                    x =>
                    {
                        splineBasedBirdController.curOffset = x;
                    })
                    .SetEase(boostCurve);

                Shaker.GlobalShakers[0].Shake(boostShake);
            }
            
            //Handles picking up feathers
            if (other.TryGetComponent(out FeatherPickup featherPickup))
            {
                featherPickup.Pickup(this.transform);
                featherPickupUE?.Invoke();
                PlayerManager.instance.AddFeather();
            }
        }

        private Tweener _slowTweener;
        private void OnCollisionEnter(Collision collision)
        {
            _slowTweener?.Kill();

            StopAllCoroutines();
            DOVirtual.Float(splineBasedBirdController.slowMult, crashedSpeedMult, crashTimeTransitionTime, value =>
            {
                splineBasedBirdController.slowMult = value;
                splineBasedBirdController.posLerpMult = crashedMoveLerpSpeed;
            }).SetEase(crashCurve).OnComplete(() => StartCoroutine(SlowReturn()));
        }

        private Tweener _returnTweener;
        private IEnumerator SlowReturn()
        {
            _returnTweener?.Kill();

            yield return new WaitForSeconds(crashedTimeLength);

            DOVirtual.Float(splineBasedBirdController.slowMult, 1, crashReturnTime, value =>
            {
                splineBasedBirdController.slowMult = value;
            }).SetEase(returnCurve).OnComplete(() =>
            {
                splineBasedBirdController.posLerpMult = 1;
            });
        }
    }
}