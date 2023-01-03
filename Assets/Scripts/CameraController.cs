using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float playerToCursorOffset;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cursorTransform;
    [SerializeField] private float cutoffDistance;

    private Vector2 lastRecordedPos;
    private  Camera _currCamera;

    private void Start()
    {
        _currCamera = Camera.main;
    }

    private void Update()
    {
        Vector2 cursorPos = _currCamera.ScreenToWorldPoint(cursorTransform.position);
        Vector2 playerPos = playerTransform.position;
        var dist = Vector2.Distance(playerPos, transform.position);
        if (dist > cutoffDistance)
        {
            lastRecordedPos = playerPos;
        }
        var targetPos = lastRecordedPos + (cursorPos - playerPos).normalized * playerToCursorOffset;
        var movePos = Vector2.Lerp(transform.position, targetPos, 0.5f);
        transform.position = new Vector3(movePos.x, movePos.y, 0) + cameraOffset;
    }
}
