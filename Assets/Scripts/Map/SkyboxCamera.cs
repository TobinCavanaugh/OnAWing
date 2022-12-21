using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Map
{
    public class SkyboxCamera : MonoBehaviour
    {
        public Camera skyboxCam;
        public Camera mainCam;

        private Transform cTransform;


        // private delegate void UpdateSkyboxDelegate(ScriptableRenderContext scriptableRenderContext, Camera camera1);
        //
        // private UpdateSkyboxDelegate _updateTheSkybox;
        //
        //
        //
        private void Start()
        {
            cTransform = mainCam.transform;
            
            //_updateTheSkybox = (context, camera1) =>  UpdateSkyBox();
            //RenderPipelineManager.beginCameraRendering += _updateTheSkybox.Invoke;
        }
        //
        // private void OnApplicationQuit()
        // {
        //
        //     RenderPipelineManager.beginCameraRendering -= _updateTheSkybox.Invoke;
        // }

        private void Update()
        {
            UpdateSkyBox();
        }

        // Update is called once per frame
        void UpdateSkyBox()
        {

                Debug.Log("Frame of rendering");
                skyboxCam.fieldOfView = mainCam.fieldOfView;
                transform.rotation = cTransform.rotation;
        }
    }
}
