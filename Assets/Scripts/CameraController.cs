using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Animator cameraAnimator;

    public void CameraShake()
    {
        var rand = Random.Range(1, 5);
        var trigger = "Shake_" + rand;
        cameraAnimator.SetTrigger(trigger);
    }
}
