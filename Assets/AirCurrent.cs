using UnityEngine;

public class AirCurrent : MonoBehaviour
{
    public Vector3 direction;
    public float boostAmount = 2f;

    public Vector3 GetBoostDirection()
    {
        return (transform.forward + direction).normalized;
    }
    
    private void OnDrawGizmos()
    {
        var dir = GetBoostDirection();
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position,  dir * boostAmount);
    }
}
