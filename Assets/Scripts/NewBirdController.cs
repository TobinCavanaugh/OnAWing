using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBirdController : MonoBehaviour
{
    public float yawSpeed = 5f;
    public float pitchSpeed = 2f;
    public float rollAmount = 35f;
    public float forwardSpeed = 5f;
    public Vector3 gravityForce = new(0, -2f, 0);
    public Rigidbody rb;
    public float maxVelocity = 25f;

    public float acceleration = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var input = new Vector3(Input.GetAxis("Vertical") * pitchSpeed, Input.GetAxis("Horizontal") * yawSpeed, 0);
        
        transform.Rotate(input, Space.Self);

        rb.velocity = Vector3.Lerp(rb.velocity, transform.up * forwardSpeed, Time.deltaTime * acceleration);
    }
}
