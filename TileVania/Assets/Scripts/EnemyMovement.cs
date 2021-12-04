using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 1f;
    [SerializeField] bool enemyDirection = true;
    Rigidbody2D enemyRigidBody;




    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        if (enemyDirection == false)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(enemyRigidBody.velocity.x)), 1f);
            moveSpeed = -moveSpeed;
        }
    }

    void Update()
    {
        enemyRigidBody.velocity = new Vector2(moveSpeed, 0f);
    }


    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRigidBody.velocity.x)), 1f);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }
}
