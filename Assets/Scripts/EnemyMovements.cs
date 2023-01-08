using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
     
    public float movementSpeed = 1.5f;

    public GameObject enemyDeathParticle;

    private Transform _enemyTransform;
    private Transform _playerTransform;
    private PlayerMovement _playerController;
    private GameController _gameController;
    private CameraController _cameraController;

    private EnemySpawner _enemySpawner;
    
    private void Start()
    {
        _enemyTransform = GetComponent<Transform>();
        _cameraController = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        _enemySpawner = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        _playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if (_gameController.isDead)
        {
            return;
        }
        if (_playerController.stopTimer > 0.01f)
        {
            _playerController.stopTimer -= Time.deltaTime;
            return;
        }
        
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
        if (other.gameObject.CompareTag("Player"))
        {
            if (!_playerController.isDashing)
            {
                _playerController.playerHealth -= 25;
                _playerController.stopTimer = 2f;
                _gameController.UpdateHealth();
                _cameraController.CameraShake();
            }
            else
            {
                _gameController.score++;
                _gameController.UpdateScore();
            }
            
            var collParticleObj = Instantiate(enemyDeathParticle, other.gameObject.transform.position, Quaternion.identity);
            collParticleObj.GetComponent<ParticleSystem>().Play();
            
            Destroy(gameObject);
            Destroy(collParticleObj, 1.2f);
            
            _enemySpawner.currEnemyCount--;
        }
    }
}
