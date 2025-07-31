using UnityEngine;

public class MyInputUser : MonoBehaviour
{
    public int player_id;
    private Rigidbody2D playerBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float JumpForce = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private float timeSinceGround = 100;
    private float timeSinceJumpInput = 100;

    private void Update()
    {
        if (CurrentlyOnGround())
        {
            timeSinceGround = 0;
            animator.SetBool("OnGround", true);
        }
        else
        {
            animator.SetBool("OnGround", false);
            timeSinceGround += Time.deltaTime;
        }

        if (timeSinceJumpInput < 0.05f && timeSinceGround < 0.05f)
        {
            timeSinceJumpInput = 10; // Set to a big number
            playerBody.linearVelocityY = JumpForce;
        }

        timeSinceJumpInput += Time.deltaTime;

        if (movingRight && movingLeft)
        {
            animator.SetBool("Walking", false);
            playerBody.linearVelocityX = 0;
        }
        else if (movingLeft)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("Walking", true);
            playerBody.linearVelocityX = -3;
        }
        else if (movingRight)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("Walking", true);
            playerBody.linearVelocityX = 3;
        }
        else
        {
            animator.SetBool("Walking", false);
            playerBody.linearVelocityX = 0;
        }
    }

    private bool CurrentlyOnGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(1, 0.01f), 0, Vector3.down, 0.5f);
        return hit.collider != null;
    }

    public void Jump()
    {
        timeSinceJumpInput = 0;
    }

    bool movingRight;
    bool movingLeft;

    public void StartRight()
    {
        movingRight = true;
    }
    public void StopRight()
    {
        movingRight = false;
    }
    public void StartLeft()
    {
        movingLeft = true;
    }
    public void StopLeft()
    {
        movingLeft = false;
    }
    public void StartDance()
    {
    }
    public void StopDance()
    {
    }
}
