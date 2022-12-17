using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace
{
    public class CameraFOVPunch : MonoBehaviour
    {
        public Camera mainCamera;

        public List<CameraPunch> presets = new();

        public CameraPunch returnPunch;

        public float lerpSpeed = 2f;

        [ReadOnly]
        public float targetFOV = 60;


        private Tweener _tweener;

        private void Update()
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * lerpSpeed);
        }

        public void CameraPunch(int index, bool autoReturn)
        {
            CameraPunch(presets[index], autoReturn);
        }

        public void CameraPunch(CameraPunch preset, bool autoReturn)
        {
            _tweener?.Kill();
            
            TweenCallback tc = null;
            if (autoReturn)
            {
                tc = () =>
                {
                    Return();
                };
            }

            _tweener = DOVirtual.Float(mainCamera.fieldOfView, preset.punchFOV, preset.punchTime,
                f =>
                {
                    mainCamera.fieldOfView = f;
                }).SetEase(preset.punchCurve).OnComplete(() => tc?.Invoke());
        }

        public void Return()
        {
            CameraPunch(returnPunch, false);
            //returnPunch.Punch(mainCamera);
        }
    }

    [Serializable]
    public class CameraPunch
    {
        public AnimationCurve punchCurve = AnimationCurve.Linear(0, 0, 1, 1);
        public float punchTime = 1f;
        public float punchFOV = 70f;
    }
}