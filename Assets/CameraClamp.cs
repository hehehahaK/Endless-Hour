using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    public Transform playerTransform;
    public float minX, maxX, minY, maxY;
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        Vector3 desiredPos = playerTransform.position;
        desiredPos.x = Mathf.Clamp(desiredPos.x, minX, maxX);
        desiredPos.y = Mathf.Clamp(desiredPos.y, minY, maxY);
        desiredPos.z = -10f;

        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
    }
}