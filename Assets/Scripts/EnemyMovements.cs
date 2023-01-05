using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    public Rigidbody2D target; 
    public Rigidbody2D enemy;
    public float enemySpeed = 1.5f;
    public float directionOffset = 45f;
    void Start()
    {
        enemy.rotation = 0f;
    }

    void Update()
    {
        Vector2 moveDirection = target.position - enemy.position;
        moveDirection.Normalize();

        enemy.rotation = Mathf.Atan2(moveDirection.y, moveDirection.x) * (Mathf.Rad2Deg + directionOffset);
        enemy.MovePosition(enemy.position + moveDirection * enemySpeed * Time.deltaTime);

    }

    void FixedUpdate()
    {

    }

    void OnCollisionEnter2D()
    {
        Destroy(gameObject);
    }
}
