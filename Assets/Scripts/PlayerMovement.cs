using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce;
    public float moveSpeed;
    private float moveX;
    private bool facingRight = true;
    
    private bool canDoubleJump;
    private bool canWallSlide;
    private bool isWallSliding;

    public LayerMask jumpableGround;

    public Transform wallCheck;
    public float wallCheckDistance;
    private bool isWallDetected;
    private bool isOnGround;
    
    private Rigidbody2D player;
    private Animator animator;
    private BoxCollider2D boxCollider;

    public enum MovementState { idle, running, jumping, falling};
    public MovementState state;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        isOnGround = IsOnGround();
        isWallDetected = IsOnWall();

        if (Input.GetButtonDown("Jump") && (isOnGround || canDoubleJump) && !isWallSliding)
        {
            player.velocity = new Vector2(player.velocity.x, jumpForce);

            if (!canDoubleJump)
                canDoubleJump = true;
            else
            {
                animator.SetTrigger("doubleJump");
                canDoubleJump = false;
            }
        }

        UpdateAnimationState();

        if(isOnGround && isWallDetected)
        {
            if(facingRight && moveX < 0)
                player.transform.Rotate(new Vector3(0, 180, 0));
            else if(!facingRight && moveX > 0)
                player.transform.Rotate(new Vector3(0, -180, 0));
        }
    }

    private void FixedUpdate()
    {
        if (!isOnGround && player.velocity.y < 0)
        {
            canWallSlide = true;
        }

        if (isWallDetected && canWallSlide && !isOnGround)
        {
            isWallSliding = true;
            canDoubleJump = true;
        }
        else
        {
            isWallSliding = false;
            player.velocity = new Vector2(moveX * moveSpeed, player.velocity.y);
        }
    }

    private void UpdateAnimationState()
    {
        if (moveX > 0f)
        {
            state = MovementState.running;

            if (!facingRight)
            {
                player.transform.Rotate(new Vector3(0, -180, 0));
            }

            facingRight = true;
        }
        else if (moveX < 0f)
        {
            state = MovementState.running;            

            if (facingRight)
            {
                player.transform.Rotate(new Vector3(0, 180, 0));
            }

            facingRight = false;
        }
        else
        {
            state = MovementState.idle;
        }

        if(player.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if(player.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int) state);
        animator.SetBool("isWallSliding", isWallSliding);
        animator.SetBool("isOnGround", isOnGround);
    }

    private bool IsOnGround()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private bool IsOnWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.left, wallCheckDistance, jumpableGround) || Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, jumpableGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
