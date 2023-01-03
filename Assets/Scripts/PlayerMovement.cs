using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float dashSpeed = 50f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 0.5f;

    private Rigidbody2D _playerBody;
    private float _dashTimer = 0f;
    private float _dashCooldownTimer = 0f;

    private void Start()
    {
        _playerBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            if (_dashTimer <= 0f && _dashCooldownTimer <= 0f)
            {
                _dashTimer = dashDuration;
                _dashCooldownTimer = dashCooldown;
            }
        }

        if (_dashTimer > 0f)
        {
            _dashTimer -= Time.deltaTime;
        }
        if (_dashCooldownTimer > 0f)
        {
            _dashCooldownTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        var movement = new Vector2(horizontalInput, verticalInput).normalized;

        if (_dashTimer > 0f)
        {
            _playerBody.MovePosition(_playerBody.position + movement * (dashSpeed * Time.fixedDeltaTime));
        }
        else
        {
            _playerBody.MovePosition(_playerBody.position + movement * (movementSpeed * Time.fixedDeltaTime));
        }
    }
}