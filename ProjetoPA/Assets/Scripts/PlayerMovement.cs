using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D playerRigidBody;
    public float movementSpeed = 5;
    private float jumpSpeed = 10;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsOnTheGround())
        {
            playerRigidBody.freezeRotation = true;
            VerticalMovement();
            HorizontalMovement();
        }
    }

    void VerticalMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerRigidBody.velocity = Vector2.up * jumpSpeed;
        }
    }

    void HorizontalMovement()
    {
        if (Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Horizontal") == 1)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += movement * Time.deltaTime * movementSpeed;
        }
    }

    bool IsOnTheGround()
    {
        List<RaycastHit2D> results = new List<RaycastHit2D>();

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(1.0f, 1.0f), 0.0f, Vector2.down, 0.1f);

        Debug.Log(results.ToString());

        return hit;
    }
}
