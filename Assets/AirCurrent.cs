using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AirCurrent : MonoBehaviour
{
    public Vector3 direction;
    public float boostAmount = 2f;

    public BoxCollider collider;


    private void Reset()
    {
        Start();
    }

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    public Vector3 GetBoostDirection()
    {
        return (transform.forward + direction).normalized;
    }
    
    private void OnDrawGizmos()
    {
        var dir = GetBoostDirection();
        Gizmos.color = Color.red;
        Gizmos.DrawRay(collider.bounds.center,  dir * boostAmount);
    }
}
