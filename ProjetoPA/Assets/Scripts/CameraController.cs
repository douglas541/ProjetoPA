using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    public float SmoothTime = 0f;
    public float MaxX = 12f;
    public float MinX = 0f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 targetPosition = Target.position;
        targetPosition.x = Mathf.Clamp(targetPosition.x, MinX, MaxX);
        targetPosition.y = transform.position.y;
        targetPosition.z = -10;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
    }
}