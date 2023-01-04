using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private CapsuleCollider2D playerCollider;
    [SerializeField] private Rigidbody2D enemyRigidBody;
    [SerializeField] private Animator playerAnimation;
    [SerializeField] private SpriteRenderer playerSprite;
    private float movementSpeed = 6;
    private float jumpSpeed = 10;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerRigidBody.freezeRotation = true;
        HorizontalMovement();
        VerticalMovement();
    }

    void VerticalMovement()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && IsOnTheGround())
        {
            playerRigidBody.velocity = Vector2.up * jumpSpeed;
            playerAnimation.SetBool("isRunning", false);
        }
    }

    void HorizontalMovement()
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            playerAnimation.SetBool("isRunning", false);
        }
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.localScale = new Vector3(-4.5727f, 4.5727f, 4.5727f);
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += movement * Time.deltaTime * movementSpeed;
            playerAnimation.SetBool("isRunning", true);

        }
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            transform.localScale = new Vector3(4.5727f, 4.5727f, 4.5727f);
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += movement * Time.deltaTime * movementSpeed;
            playerAnimation.SetBool("isRunning", true);
        }
    }

    bool IsOnTheGround()
    {
        RaycastHit2D checkCollision = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayerMask);

        if (checkCollision.collider != null)
        {
            return true;
        }

        return false;
    }
}
