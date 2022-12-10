using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace
{
    public class BirdWing : MonoBehaviour
    {
        public float forceMult;
        public float torqueMult;
        
        public Rigidbody mainBody;

        [SerializeField, ReadOnly]
        private bool isWarm = false;

        public Vector3 torqueAxis = Vector3.up;
        public Vector3 forceAxis = new(0, 1, .5f);

        public bool requireWarming = true;

        public void Warm()
        {
            Debug.Log("WARMED");
            isWarm = true;
        }

        public void Cool()
        {
            if (isWarm || !requireWarming)
            {
                float v = 1f;
                if (!requireWarming)
                {
                    v = Input.GetAxis("TWing");
                }
                mainBody.AddRelativeTorque(torqueMult * torqueAxis * v);
                mainBody.AddRelativeForce(forceAxis * forceMult);
                isWarm = false;
            }
        }
    }
}