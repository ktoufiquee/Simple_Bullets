using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float movementSpeed = 10f;
    public float maxPlayerSpeed = 20f;
    public float dashSpeed = 50f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 0.5f;
    public float bulletSpeed = 75f;
    public float bulletCooldown = 0.1f;

    private Rigidbody2D _playerBody;
    private float _dashLength = 0f;
    private float _dashTimer = 0f;
    private float _bulletTimer = 0f;
    private Camera _gameCamera;

    private void Start()
    {
        _playerBody = GetComponent<Rigidbody2D>();
        _gameCamera = Camera.main;
    }

    private void Update()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            if (_dashLength <= 0f && _dashTimer <= 0f)
            {
                _dashLength = dashDuration;
                _dashTimer = dashCooldown;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (_bulletTimer <= 0f)
            {
                _bulletTimer = bulletCooldown;
                Fire();
            }
        }

        if (_dashLength > 0f)
        {
            _dashLength -= Time.deltaTime;
        }
        if (_dashTimer > 0f)
        {
            _dashTimer -= Time.deltaTime;
        }
        if (_bulletTimer > 0f)
        {
            _bulletTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        var normalizedInput = new Vector2(horizontalInput, verticalInput).normalized;

        if (_dashLength > 0f)
        {
            _playerBody.velocity = normalizedInput * dashSpeed;
        }
        else
        {
            _playerBody.velocity = normalizedInput * movementSpeed;
        }

        // if (_playerBody.velocity.magnitude > maxPlayerSpeed)
        // {
        //     _playerBody.velocity = _playerBody.velocity.normalized * maxPlayerSpeed;
        // }
    }

    private void Fire()
    {
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        var bulletBody = bullet.GetComponent<Rigidbody2D>();
        var mousePos = (Vector2) _gameCamera.ScreenToWorldPoint(Input.mousePosition);
        var direction = (mousePos - _playerBody.position).normalized;
        bulletBody.velocity = direction * bulletSpeed;
        Destroy(bullet, 3f);
    }
}