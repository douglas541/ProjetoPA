using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private CapsuleCollider2D playerCollider;
    [SerializeField] private Rigidbody2D enemyRigidBody;
    private float movementSpeed = 6;
    private float jumpSpeed = 10;
    List<RaycastHit2D> results = new List<RaycastHit2D>();
    ContactFilter2D contactFilter2D = new ContactFilter2D();
    bool enemyHit = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerRigidBody.freezeRotation = true;
        HorizontalMovement();
        VerticalMovement();
        EnemyHit();
    }

    void VerticalMovement()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && IsOnTheGround())
        {
            playerRigidBody.velocity = Vector2.up * jumpSpeed;
        }
    }

    void HorizontalMovement()
    {
        if ((Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Horizontal") == 1))
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += movement * Time.deltaTime * movementSpeed;
        }
    }

    bool IsOnTheGround()
    {
        //RaycastHit2D hit2 = Physics2D.BoxCast(transform.position, new Vector2(1.0f, 1.0f), 0.0f, Vector2.down, 0.1f);
        //int hit = Physics2D.BoxCast(transform.position, new Vector2(1.0f, 1.0f), 0.0f, Vector2.down, contactFilter2D, results, 1.0f);

        RaycastHit2D checkCollision = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayerMask);
        
        return checkCollision.collider != null;
    }

    bool EnemyHit()
    {
        bool initialEnemyHit = enemyHit;
        enemyHit = false;
        int hit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0.0f, Vector2.down, contactFilter2D, results, 1.0f);

        results.ForEach(result =>
        {
            if (result.rigidbody == enemyRigidBody)
            {
                enemyHit = true;
            }
        });

        if (initialEnemyHit != enemyHit)
        {
            Debug.Log(enemyHit);
        }

        return enemyHit;
    }
}
