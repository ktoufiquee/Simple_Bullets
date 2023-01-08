using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    public int currEnemyCount;
    private Vector3[] _randomPoint;
    private GameController _gameController;
    private bool _targetsSpawned;
    private int _totalSpawned;
    
    private void Start()
    {
        _totalSpawned = 0;
        _targetsSpawned = false;
        currEnemyCount = 0;
        _randomPoint = new Vector3[4];
        _gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        var rand = Random.Range(-0.1f, 1.1f);
        _randomPoint[0] = new Vector3(-0.1f, rand, 0);
        _randomPoint[1] = new Vector3(1.1f, rand, 0);
        _randomPoint[2] = new Vector3(rand, -0.1f, 0);
        _randomPoint[3] = new Vector3(rand, 1.1f, 0);
        var spawnPoint = Camera.main.ViewportToWorldPoint(_randomPoint[Random.Range(0, _randomPoint.Length)]);
        spawnPoint.z = 0;
        if (_totalSpawned < _gameController.enemyCount && currEnemyCount < _gameController.maxEnemyAllowedOnScreen) {
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoint, Quaternion.identity);
            _totalSpawned++;
            currEnemyCount++;
            if (_totalSpawned == _gameController.enemyCount)
            {
                _targetsSpawned = true;
            }
        }
        
        if (_targetsSpawned && currEnemyCount == 0)
        {
            _targetsSpawned = false;
            StartCoroutine(InvokeWaveUpdate());
        }
    }

    private IEnumerator InvokeWaveUpdate()
    {
        yield return new WaitForSeconds(3f);
        _gameController.UpdateWave();
        _totalSpawned = 0;
    }
}
