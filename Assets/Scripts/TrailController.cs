using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    private TrailRenderer _trail;
    private PlayerMovement _playerController;

    private readonly Color _defaultColor = new Color(53, 204, 153, 1);
    private readonly Color _dashColor = new Color(204, 102, 102, 1);

    private void Start()
    {
        _trail = GetComponent<TrailRenderer>();
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        _trail.time = _playerController.isDashing ? 5f : 2f;
        // _trail.startColor = _playerController.isDashing ? _dashColor: _defaultColor;
    }
}
