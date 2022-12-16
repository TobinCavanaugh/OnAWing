using UnityEngine;

public class SkyboxCamera : MonoBehaviour
{
    public Camera skyboxCam;
    public Camera mainCam;

    private Transform cTransform;
    private void Start()
    {
        cTransform = mainCam.transform;
    }

    // Update is called once per frame
    void Update()
    {
        skyboxCam.fieldOfView = mainCam.fieldOfView;
        transform.rotation = cTransform.rotation;
    }
}
