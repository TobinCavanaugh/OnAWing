using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class AnimBasedBirdController : MonoBehaviour
    {
        public Animator leftWing;
        public Animator rightWing;
        public Animator tailWing;

        public string valueName = "WingAmount";

        public float gravity = 4f;
        public Rigidbody rigidbody;

        private void Update()
        {
            leftWing.SetFloat(valueName, Input.GetAxis("Horizontal")/2f + .5f);
            rightWing.SetFloat(valueName, Input.GetAxis("Horizontal")/2f + .5f);
            tailWing.SetFloat(valueName, Input.GetAxis("TWing")/2f + .5f);
            rigidbody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }
    }
}