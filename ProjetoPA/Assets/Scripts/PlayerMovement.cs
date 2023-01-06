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
    private float spriteSize = 4.5727f;

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
            playerAnimation.SetBool("isRunning", false);
            playerAnimation.SetBool("isCrouching", false);
            playerRigidBody.velocity = Vector2.up * jumpSpeed;
        }

        Crouch();
    }

    void HorizontalMovement()
    {
        //The following conditions detects if the program has any low diagonal input. If not, it will proceed.
        if(!(Input.GetKey(KeyCode.S)) && !(Input.GetKey(KeyCode.DownArrow)) && IsOnTheGround())
        {
            HorizontalInput();
        }
        if (IsOnTheGround() == false)
        {
            HorizontalInput();
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

    void Crouch()
    {
        if(playerAnimation.GetBool("isRunning") == true && playerAnimation.GetBool("isCrouching") == true) {
            playerAnimation.SetBool("isRunning", false);
        }

        if((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && IsOnTheGround()) {
            playerAnimation.SetBool("isCrouching", true);
        }
    }

    void HorizontalInput()
    {
        playerAnimation.SetBool("isCrouching", false);
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            playerAnimation.SetBool("isCrouching", false);
            playerAnimation.SetBool("isRunning", false);
        }
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.localScale = new Vector3(-spriteSize, spriteSize, spriteSize);
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += movement * Time.deltaTime * movementSpeed;
            playerAnimation.SetBool("isRunning", true);
        }
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            transform.localScale = new Vector3(spriteSize, spriteSize, spriteSize);
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += movement * Time.deltaTime * movementSpeed;
            playerAnimation.SetBool("isRunning", true);
        }
    }
}
