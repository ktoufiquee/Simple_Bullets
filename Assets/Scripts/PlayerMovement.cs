using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isDashing;
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    [SerializeField] private int dashCount;
    [SerializeField] private GameObject playerGun;
    [SerializeField] private GameObject gunPrefab;

    private float _dashDurationTimer;
    private float _dashCooldownTimer;

    private Rigidbody2D _playerBody;
    private Animator _playerAnim;
    private Camera _gameCamera;
    
    private void Start()
    {
        _playerBody = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<Animator>();
        _gameCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (_dashDurationTimer <= 0 && _dashCooldownTimer <= 0 && dashCount > 0)
            {
                _dashDurationTimer = dashDuration;
                _dashCooldownTimer = dashCooldown;
                --dashCount;
                // if (dashCount == 0)
                // {
                //     playerGun.SetActive(false);
                // }
                StartCoroutine(SpawnGun(transform.position));
            }
        }
        
        if (_dashDurationTimer > 0)
        {
            _dashDurationTimer -= Time.deltaTime;
        }
        if (_dashCooldownTimer > 0)
        {
            _dashCooldownTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        var mousePos = (Vector2)_gameCamera.ScreenToWorldPoint(Input.mousePosition);
        var mouseDirection = (mousePos - _playerBody.position).normalized;
        var inputPos = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (_dashDurationTimer > 0)
        {
            _playerBody.MovePosition(_playerBody.position + mouseDirection * (dashSpeed * Time.fixedDeltaTime));
        }
        else
        {
            _playerBody.MovePosition(_playerBody.position + inputPos * (movementSpeed * Time.fixedDeltaTime));
        }

        transform.localScale = mousePos.x < transform.position.x ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        isDashing = _dashDurationTimer > 0;
        _playerAnim.SetFloat("Speed", inputPos.magnitude);
        _playerAnim.SetBool("isDashing", isDashing);

        if (!isDashing && dashCount > 0)
        {
            playerGun.SetActive(true);
        }
        else
        {
            playerGun.SetActive(false);
        }
    }

    // private void OnCollisionEnter2D(Collision2D col)
    // {
    //     
    //     if (col.gameObject.CompareTag("Gun"))
    //     {
    //         if (dashCount == 0)
    //         {
    //             playerGun.SetActive(true);
    //         }
    //         dashCount++;
    //         Destroy(col.gameObject);
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Triggered in Player");
        if (col.CompareTag("Gun"))
        {
            // if (dashCount == 0)
            // {
            //     playerGun.SetActive(true);
            // }
            dashCount++;
            Destroy(col.gameObject);
        }
    }

    // public void ToggleGun()
    // {
    //     playerGun.SetActive(!playerGun.activeSelf);
    // }

    private IEnumerator SpawnGun(Vector3 pos)
    {
        yield return new WaitForSeconds(dashDuration / 5);
        Instantiate(gunPrefab, pos, Quaternion.identity);
    }
}