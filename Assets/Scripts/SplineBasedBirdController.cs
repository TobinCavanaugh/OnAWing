using System;
using Dreamteck.Splines;
using UnityEditor.Experimental;
using UnityEngine;

namespace DefaultNamespace
{
    public class SplineBasedBirdController : MonoBehaviour
    {
        public SplineFollower spineFollower;

        [Space]
        public float moveMult = .01f;
        public float lerpSpeed = .1f;
        public float maxRotation = 45f;
        public float lerpRot = .1f;


        [Space]
        public float posLerpSpeed = 5f;
        public float posXMult = 16;
        public float posYMult = 9;
        public Camera gameCamera;
        public Transform birdBody;

        [Space]
        public Animator animator;
        public string idleClip = "BirdIdle";
        public string flapClip = "BirdWarm";
        public float crossfadeTime = .1f;

        private Vector3 mouseInput;

        private float lerpedValue = 0;
        private void Update()
        {
            spineFollower.followSpeed = Mathf.Lerp(spineFollower.followSpeed, Input.GetAxis("Vertical") * moveMult,
                lerpSpeed * Time.deltaTime);

            mouseInput = GetMouseInput();
            birdBody.localPosition =
                Vector3.Lerp(birdBody.localPosition, gameCamera.ScreenToViewportPoint(mouseInput), Time.deltaTime * posLerpSpeed);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName(idleClip))
                {
                    animator.CrossFade(flapClip, crossfadeTime);
                }
            }
            
            var xrot = (mouseInput.normalized.x) * maxRotation;
            var bblea = birdBody.localEulerAngles;
            lerpedValue = Mathf.Lerp(lerpedValue, xrot, Time.deltaTime * lerpRot);
            birdBody.localEulerAngles = new(bblea.x, bblea.y, lerpedValue);

            //birdBody.localEulerAngles = new(bblea.x, bblea.y, Mathf.Lerp(bblea.x, xrot, Time.deltaTime * lerpRot));

            //birdBody.localEulerAngles = Vector3.Lerp(birdBody.localEulerAngles, new(bblea.x, bblea.y, xrot),
            //    Time.deltaTime * lerpRot);
            //birdBody.localEulerAngles = new(Mathf.Lerp(birdBody.localEulerAngles.x, xrot, Time.deltaTime * lerpRot), bblea.y, xrot);
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