using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxCamera : MonoBehaviour
{
    public Transform mainCam;
    
    // Update is called once per frame
    void Update()
    {
        transform.rotation = mainCam.rotation;
    }
}
