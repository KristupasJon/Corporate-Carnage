using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;

    void FixedUpdate()
    {
        Vector3 desiredPosition = new(target.position.x, target.position.y, -10);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
