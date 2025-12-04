using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomLvl4 : MonoBehaviour
{
    private Camera Cam;

    public float TargetZoom = 3;
    private float ScrollData;       
    public float ZoomSpeed = 3;
    void Start()
    {
        Cam = GetComponent<Camera>();
        TargetZoom = Cam.orthographicSize; 
    }
    void Update()
    {
        ScrollData = Input.GetAxis("Mouse ScrollWheel");
        TargetZoom = TargetZoom - ScrollData;
        TargetZoom = Mathf.Clamp(TargetZoom, 3, 6);
        Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, TargetZoom, Time.deltaTime * ZoomSpeed);
    }
}