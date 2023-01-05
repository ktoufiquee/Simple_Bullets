using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int enemyCount;
    private Vector3[] _randomPoint;
    public static int EnemyCount = 0;
    
    private void Start()
    {
        _randomPoint = new Vector3[4];
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
        if(EnemyCount < enemyCount) {
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoint, Quaternion.identity);
            EnemyCount++;
        }
    }
}
