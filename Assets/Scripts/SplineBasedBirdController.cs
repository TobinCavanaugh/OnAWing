using System;
using Dreamteck.Splines;
using MilkShake;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class SplineBasedBirdController : MonoBehaviour
    {
        public SplineFollower splineFollower;
        public float splinePosLerpSpeed = 1f;
        public float splineRotLerpSpeed = 1f;
        public float boostedSpeed = 55f;
        public float valueLerpSpeed = 2f;
        public float boostTimeLength = 3f;
        public float defaultSpeed = 45f;
        public ShakePreset boostShake;
        
        [SerializeField, ReadOnly]
        private float curTime = 0;
        
        [Space]
        public float maxRotation = 45f;
        public float lerpRot = .1f;
        
        [Space]
        public float posLerpSpeed = 5f;
        public float posXMult = 16;
        public float posYMult = 9;
        public Camera gameCamera;
        public Transform birdBody;
        
        public Vector3 curOffset;

        [Space]
        public Animator animator;
        public string idleClip = "BirdIdle";
        public string flapClip = "BirdWarm";
        public float crossfadeTime = .1f;

        public CameraFOVPunch cameraFOVPunch;

        [Space]
        public BirdAnimationController birdAnimationController;

        private Vector3 mouseInput;

        private float lerpedValue = 0;
        private float curMoveSpeed = 0f;
        private static readonly int DoBoost = Animator.StringToHash("DoBoost");

        private void Update()
        {
            mouseInput = GetMouseInput();
            birdBody.localPosition =
                Vector3.Lerp(birdBody.localPosition, gameCamera.ScreenToViewportPoint(mouseInput), Time.deltaTime * posLerpSpeed);

            curMoveSpeed = defaultSpeed;
            
            //Boosting
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName(idleClip))
                {
                    animator.CrossFade(flapClip, crossfadeTime);
                }
                if (birdAnimationController.wingInPosition)
                {
                    curTime = 0;
                    animator.SetBool(DoBoost, true);
                    curMoveSpeed = boostedSpeed;
                    Shaker.GlobalShakers[0].Shake(boostShake);
                    //cameraFOVPunch.Punch();
                }
                else
                {
                    animator.SetBool(DoBoost, false);
                }
            }

            //For Boost
            curTime += Time.deltaTime;
            if (curTime <= boostTimeLength)
            {
                curMoveSpeed = boostedSpeed;
            }

            
            splineFollower.followSpeed = Mathf.Lerp(splineFollower.followSpeed, curMoveSpeed, Time.deltaTime * valueLerpSpeed);
            
            
            //Bird body movement and rotation
            var xrot = (mouseInput.normalized.x) * maxRotation;
            var bblea = birdBody.localEulerAngles;
            lerpedValue = Mathf.Lerp(lerpedValue, xrot, Time.deltaTime * lerpRot);
            birdBody.localEulerAngles = new(bblea.x, bblea.y, lerpedValue);

            
            //Following splinefollower
            transform.position = Vector3.Lerp(transform.position, splineFollower.transform.position + curOffset,
                Time.deltaTime * splinePosLerpSpeed);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, splineFollower.transform.rotation,
                Time.deltaTime * splineRotLerpSpeed);
        }

        private Vector3 GetMouseInput()
        {
            var imp = Input.mousePosition;

            imp.x -= Screen.width / 2f;
            imp.y -= Screen.height / 2f;


            imp.x *= posXMult;
            imp.y *= posYMult;

            return new Vector3(imp.x, imp.y, 0);
        }
    }
}