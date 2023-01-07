using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
     
    public float movementSpeed = 1.5f;

    private Transform _enemyTransform;
    private Transform _playerTransform;
    private PlayerMovement _playerController;
    
    private void Start()
    {
        _enemyTransform = GetComponent<Transform>();
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        _playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        var enemyPos = _enemyTransform.position;
        var playerPos = _playerTransform.position;

        transform.localScale = playerPos.x < enemyPos.x ? new Vector3(-1, -1, 1) : new Vector3(-1, 1, 1);
        
        var moveDirection = (playerPos - enemyPos).normalized;
        var angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * (Mathf.Rad2Deg);
        _enemyTransform.rotation = Quaternion.Euler(0, 0, angle);
        _enemyTransform.position = Vector2.MoveTowards(enemyPos, playerPos, movementSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && _playerController.isDashing)
        {
            Destroy(gameObject);
        }
    }
}
