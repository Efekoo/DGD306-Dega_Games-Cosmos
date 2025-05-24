using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek oyuncu
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Z ekseni sabit kalýr
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothed;
    }
}