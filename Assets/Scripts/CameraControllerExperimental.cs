using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerExperimental : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    public float mouseSensitivity = 2f;
    public Vector3 offset;
    
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _targetPos;

    private void LateUpdate()
    {
        var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        offset += new Vector3(mouseX, mouseY, 0);
        _targetPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, _targetPos, ref _velocity, smoothTime);
    }

}
