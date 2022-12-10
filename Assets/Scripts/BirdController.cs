using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float lerpSpeed = 1f;
    public float maxAngles = 28f;

    
    [Space]
    public Transform rWing;
    public Transform lWing;
    public Transform tWing;

    [Space] 
    public Rigidbody rigidbody;
    public float forceMult = 5f;
    public Vector3 baseRotOffset = new(0, 0.07f, 0.09f);

    [ShowInInspector, ReadOnly]
    private Vector3[] eulerAngles = new Vector3[3];
    private void Start()
    {
        eulerAngles[0] = rWing.localEulerAngles;
        eulerAngles[1] = lWing.localEulerAngles;
        eulerAngles[2] = tWing.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        var lerpStep = Time.deltaTime * lerpSpeed;
        LerpWing(lWing, maxAngles * Input.GetAxis("LWing"), lerpStep);
        LerpWing(rWing, maxAngles * Input.GetAxis("RWing"), lerpStep);
        AddWingForce();
    }

    void AddWingForce()
    {
        
    }

    
    private void LerpWing(Transform wing, float newValue, float lerpStep)
    {
        var lea = wing.localEulerAngles;
        wing.localEulerAngles = new Vector3(newValue, lea.y, lea.z);
    }
}
