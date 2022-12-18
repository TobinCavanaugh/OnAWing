using Dreamteck.Splines;
using MilkShake;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class SplineBasedBirdController : MonoBehaviour
    {
        #region DEFAULT_MOVEMENT

        private const string DEFAULT_MOVEMENT = "Default Movement";
        
        [FoldoutGroup(DEFAULT_MOVEMENT)]
        public SplineFollower splineFollower;
        
        [FoldoutGroup(DEFAULT_MOVEMENT)]
        public float splinePosLerpSpeed = 1f;
        
        [FoldoutGroup(DEFAULT_MOVEMENT)]
        public float splineRotLerpSpeed = 1f;
        
        [FoldoutGroup(DEFAULT_MOVEMENT)]
        public float defaultSpeed = 45f;        

        [FoldoutGroup(DEFAULT_MOVEMENT), ReadOnly]
        public float slowMult = 1;

        [FormerlySerializedAs("_curMoveSpeed")] [FoldoutGroup(DEFAULT_MOVEMENT), ReadOnly]
        public float curMoveSpeed = 0f;

        #endregion

        #region BOOSTED

        private const string BOOSTED = "Boosted";
        
        [FoldoutGroup(BOOSTED)]
        public float boostedSpeed = 55f;
        
        [FoldoutGroup(BOOSTED)]
        public float valueLerpSpeed = 2f;

        [FoldoutGroup(BOOSTED)]
        public float boostTimeLength = 3f;
        
        [FoldoutGroup(BOOSTED)]
        public ShakePreset boostShake;
        
        [FoldoutGroup(BOOSTED)]
        public AnimationCurve boostSpeedCurve = AnimationCurve.Linear(0,0, 1, 1);

        [SerializeField, FoldoutGroup(BOOSTED), ReadOnly]
        private float curTime = 0;

        #endregion
        
        #region POSITIONING

        private const string POSITIONING = "Mouse Positioning";
        
        [FoldoutGroup(POSITIONING)]
        public float posLerpSpeed = 5f;

        [FoldoutGroup(POSITIONING)] 
        public float posLerpMult = 1f;
        
        [FoldoutGroup(POSITIONING)]
        public float posXMult = 16;
        
        [FoldoutGroup(POSITIONING)]
        public float posYMult = 9;
        
        [FoldoutGroup(POSITIONING)]
        public Camera gameCamera;
        
        [FoldoutGroup(POSITIONING)]
        public Transform birdBody;
        
        [FoldoutGroup(POSITIONING), ReadOnly]
        public Vector3 curOffset;
        
        [FoldoutGroup(POSITIONING)]
        public float maxRotation = 45f;
        
        [FoldoutGroup(POSITIONING)]
        public float lerpRot = .1f;
        
        #endregion

        #region ANIMATOR

        private const string ANIMATOR = "Animator";
        
        [FoldoutGroup(ANIMATOR)]
        public BirdAnimationController birdAnimationController;

        [FoldoutGroup(ANIMATOR)]
        public Animator animator;
        
        [FoldoutGroup(ANIMATOR)]
        public string idleClip = "BirdIdle";
        
        [FoldoutGroup(ANIMATOR)]
        public string flapClip = "BirdWarm";
        
        [FoldoutGroup(ANIMATOR)]
        public float crossfadeTime = .1f;        

        #endregion

        private Vector3 _mouseInput;
        private float _lerpedValue = 0;
        private static readonly int DoBoost = Animator.StringToHash("DoBoost");

        private void Start()
        {
            curTime = boostTimeLength;
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void Update()
        {
            
            _mouseInput = GetMouseInput();
            birdBody.localPosition =
                Vector3.Lerp(birdBody.localPosition, gameCamera.ScreenToViewportPoint(_mouseInput), Time.deltaTime * posLerpSpeed);

            curMoveSpeed = defaultSpeed;
            
            //Boosting
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //If idling
                if (animator.GetCurrentAnimatorStateInfo(0).IsName(idleClip))
                {
                    animator.CrossFade(flapClip, crossfadeTime);
                }
                //If the wing is in position, do the boost!
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

            //If boost is active, continue to set the time
            curTime += Time.deltaTime;
            if (curTime <= boostTimeLength)
            {
                curMoveSpeed = (boostSpeedCurve.Evaluate(curTime / 1f) + 1) * boostedSpeed;
                //curMoveSpeed = boostedSpeed;
            }

            //Set the follow speed
            splineFollower.followSpeed = Mathf.Lerp(splineFollower.followSpeed, curMoveSpeed * slowMult, Time.deltaTime * valueLerpSpeed * posLerpMult);
            
            //Bird body movement and rotation
            var xrot = (_mouseInput.normalized.x) * maxRotation;
            var bblea = birdBody.localEulerAngles;
            _lerpedValue = Mathf.Lerp(_lerpedValue, xrot, Time.deltaTime * lerpRot);
            birdBody.localEulerAngles = new(bblea.x, bblea.y, _lerpedValue);

            
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