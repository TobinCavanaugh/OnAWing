using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class CameraFOVPunch : MonoBehaviour
    {
        public float defaultFOV = 60f;
        public Camera mainCamera;

        public List<CameraPunch> presets = new();

        public CameraPunch returnPunch;
        
        public void Punch(CameraPunch punch, CameraPunch followUp = null)
        {
            punch.Punch(mainCamera, followUp);
        }
        
        public void Punch(int preset, int followUp = -1)
        {
            CameraPunch fu = null;
            if (followUp <= presets.Count - 1)
            {
                fu = presets[followUp];
            }

            Punch(presets[preset], fu);
        }

        public void Return()
        {
            returnPunch.Punch(mainCamera);

        }
        
        
            
    }

    [Serializable]
    public class CameraPunch
    {
        public AnimationCurve punchCurve = AnimationCurve.Linear(0, 0, 1, 1);
        public float punchTime = 1f;
        public float punchFOV = 70f;

        public void Punch(Camera camera, CameraPunch followUp = null)
        {
            DOVirtual.Float(camera.fieldOfView, punchFOV, punchTime, b =>
            {
                camera.fieldOfView = b;
            }).SetEase(punchCurve).OnComplete(() => followUp?.Punch(camera, null));
        }
    }
}