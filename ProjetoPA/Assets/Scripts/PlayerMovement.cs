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
    private float _movementSpeed = 6;
    private float _jumpSpeed = 10;
    private float _spriteSize = 4.5727f;
    private bool _canMove = true;

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

    public bool GetPlayerCanMove()
    {
        return this._canMove;
    }

    public void SetPlayerCanMove(bool canMove)
    {
        this._canMove = canMove;
    } 

    void VerticalMovement()
    {
        if(Input.GetAxisRaw("Jump") > 0 && IsOnTheGround() && _canMove)
        {
            Jump();
        }
        if(Input.GetAxisRaw("Crouch") < 0 && IsOnTheGround())
        {
            Crouch();
        }
        else
        {
            playerAnimation.SetBool("isCrouching", false);
        }
    }

    void Jump()
    {
        playerAnimation.SetBool("isRunning", false);
        playerAnimation.SetBool("isCrouching", false);
        playerRigidBody.velocity = Vector2.up * _jumpSpeed;
    }

    void HorizontalMovement()
    {
        //The following conditions detects if the program has any low diagonal input. If not, it will proceed.
        if (!(Input.GetKey(KeyCode.S)) && !(Input.GetKey(KeyCode.DownArrow)) && IsOnTheGround() && _canMove || IsOnTheGround() == false)
        {
            HandleHorizontalMovement();
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
        playerAnimation.SetBool("isCrouching", true);
    }


    void HandleHorizontalMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput == 0)
        {
            playerAnimation.SetBool("isRunning", false);
        }
        else
        {
            transform.localScale = new Vector3(horizontalInput * _spriteSize, _spriteSize, _spriteSize);
            Vector3 movement = new Vector3(horizontalInput, 0f, 0f);
            transform.position += movement * Time.deltaTime * _movementSpeed;
            playerAnimation.SetBool("isRunning", true);
        }
    }
}
