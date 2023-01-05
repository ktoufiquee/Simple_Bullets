using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float movementSpeed = 10f;
    public float maxPlayerSpeed = 20f;
    public float dashForce = 10f;
    public float bulletSpeed = 75f;
    public float maxDashDistance = 5f;
    
    
    private Rigidbody2D _playerBody;
    private Camera _gameCamera;

    public bool isDashing = false;
    
    private void Start()
    {
        _playerBody = GetComponent<Rigidbody2D>();
        _gameCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !isDashing)
        {
            Dash();
        }
    }

    private void FixedUpdate()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        
        var normalizedInput = new Vector2(horizontalInput, verticalInput).normalized;
        
        _playerBody.velocity = normalizedInput * movementSpeed;
        if (_playerBody.velocity.magnitude > maxPlayerSpeed & !isDashing)
        {
            _playerBody.velocity = _playerBody.velocity.normalized * maxPlayerSpeed;
        }
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

    private void Dash()
    {
        
        var cursorPos = _gameCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dashDirection = cursorPos - transform.position;
        var dashDistance = Vector2.Distance(cursorPos, transform.position);

        dashDistance = Mathf.Min(dashDistance, maxDashDistance);
        dashDirection = dashDirection.normalized * dashDistance;
        
        _playerBody.AddForce(dashDirection * dashForce);
        
        Debug.Log(dashDirection * dashForce);
        
        // anim.SetTrigger("Dash");
        isDashing = true;

    }
}