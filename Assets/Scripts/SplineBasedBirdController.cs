using Dreamteck.Splines;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class SplineBasedBirdController : MonoBehaviour
    {
        public SplineFollower splineFollower;
        
        public float defaultSpeed = 45;
        public float boostedSpeed = 55f;
        public float valueLerpSpeed = 2f;
        public float boostTimeLength = 3f;
        
        [SerializeField, ReadOnly]
        private float curTime = 0;
        
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

        [Space]
        public BirdAnimationController birdAnimationController;

        private Vector3 mouseInput;

        private float lerpedValue = 0;
        private void Update()
        {
            splineFollower.followSpeed = Mathf.Lerp(splineFollower.followSpeed, Input.GetAxis("Vertical") * moveMult,
                lerpSpeed * Time.deltaTime);

            mouseInput = GetMouseInput();
            birdBody.localPosition =
                Vector3.Lerp(birdBody.localPosition, gameCamera.ScreenToViewportPoint(mouseInput), Time.deltaTime * posLerpSpeed);

            float followSpeed = defaultSpeed;
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName(idleClip))
                {
                    animator.CrossFade(flapClip, crossfadeTime);
                }
                if (birdAnimationController.wingInPosition)
                {
                    curTime = 0;
                    followSpeed = boostedSpeed;
                }
            }

            curTime += Time.deltaTime;

            if (curTime <= boostTimeLength)
            {
                followSpeed = boostedSpeed;
            }
            
            

            splineFollower.followSpeed = Mathf.Lerp(splineFollower.followSpeed, followSpeed, Time.deltaTime * valueLerpSpeed);
            
            var xrot = (mouseInput.normalized.x) * maxRotation;
            var bblea = birdBody.localEulerAngles;
            lerpedValue = Mathf.Lerp(lerpedValue, xrot, Time.deltaTime * lerpRot);
            birdBody.localEulerAngles = new(bblea.x, bblea.y, lerpedValue);
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