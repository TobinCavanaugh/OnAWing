using UnityEngine;

public class RotationLock : MonoBehaviour
{

    public Vector3 lockedEulerAngles = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = lockedEulerAngles;
    }
}
