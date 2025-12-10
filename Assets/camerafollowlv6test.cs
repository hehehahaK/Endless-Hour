using UnityEngine;

public class camerafollowlv6test : MonoBehaviour
{
    public Transform target; // Atreus
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 0, -10);
    
    // World bounds
    public float minX = -0.7f;
    public float maxX = 34.6f;
    public float minY = 0f;
    public float maxY = 15f;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        // Clamp camera within bounds
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minX, maxX);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minY, maxY);
        smoothedPosition.z = offset.z;
        
        transform.position = smoothedPosition;
    }
}   
