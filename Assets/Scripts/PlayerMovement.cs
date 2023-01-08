using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isDashing;
    
    public float movementSpeed;
    [SerializeField] private float dashSpeed;
    public float dashDuration;
    [SerializeField] private float dashCooldown;
    [SerializeField] private int dashCount;
    [SerializeField] private GameObject playerGun;
    [SerializeField] private GameObject gunPrefab;

    private Vector2 _mousePos;
    private Vector2 _shootPos;
    public float stopTimer = 0f;
    
    private float _dashDurationTimer;
    private float _dashCooldownTimer;

    private Rigidbody2D _playerBody;
    private Animator _playerAnim;
    private Camera _gameCamera;

    public int playerHealth = 100;
    
    private void Start()
    {
        _playerBody = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<Animator>();
        _gameCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
        {
            if (_dashDurationTimer <= 0 && _dashCooldownTimer <= 0 && dashCount > 0)
            {
                _dashDurationTimer = dashDuration;
                _dashCooldownTimer = dashCooldown;
                --dashCount;
                _mousePos = (Vector2)_gameCamera.ScreenToWorldPoint(Input.mousePosition);
                _shootPos = _playerBody.position;
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
        
        var mouseDirection = (_mousePos - _shootPos).normalized;
        var inputPos = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        
        if (_dashDurationTimer > 0)
        {
            if (mouseDirection.magnitude < 7f)
            {
                mouseDirection *= 5;
                mouseDirection = mouseDirection.normalized;
            }
            _playerBody.MovePosition(_playerBody.position + mouseDirection * (dashSpeed * Time.fixedDeltaTime));
        }
        else
        {
            _playerBody.MovePosition(_playerBody.position + inputPos * (movementSpeed * Time.fixedDeltaTime));
        }
        var latestMousePos = (Vector2)_gameCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.localScale = latestMousePos.x < transform.position.x ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.CompareTag("Gun"))
        {
            if (dashCount == 0)
            {
                playerGun.SetActive(true);
            }
            dashCount++;
            Destroy(col.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
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