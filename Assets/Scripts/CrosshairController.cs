using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    private RectTransform _crosshairTransform;

    private void Start()
    {
        _crosshairTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        _crosshairTransform.position = Input.mousePosition;
    }
}