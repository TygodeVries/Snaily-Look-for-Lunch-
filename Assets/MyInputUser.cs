using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MyInputUser : MonoBehaviour
{
    public int player_id;

    public AudioClip JumpSound;

    private Rigidbody2D playerBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private AudioSource audioSource;
    private float JumpForce = 7.5f;

    private float startTime;
    bool rewinding = false;
    public struct state
    {
        public float time;
        public Vector3 position;
        public bool flipX;
        public bool onGround;
        public bool lifting;
    };
    public state SaveCurrentState()
    {
        state s;
        s.time = Time.time - startTime;
        s.position = transform.position;
        s.flipX = spriteRenderer.flipX;
        s.onGround = animator.GetBool("OnGround");
        s.lifting = animator.GetBool("Platform");
		return s;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startTime = Time.time;
        animator = GetComponentInChildren<Animator>();
        playerBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponentInChildren<AudioSource>();
    }

    private float timeSinceGround = 100;
    private float timeSinceJumpInput = 100;
    public void SetState(state state)
    {
        rewinding = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.position = state.position;
        animator.SetBool("OnGround", state.onGround);
        spriteRenderer.flipX = state.flipX;
		animator.SetBool("Platform", state.lifting);

	}
	private void Update()
    {
        if (!rewinding)
        {
            if (platformMode)
                return;

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
                GetComponentInChildren<ParticleSystem>().Play();
                audioSource.pitch = Random.Range(0.9f, 1.111f);
                audioSource.PlayOneShot(JumpSound);

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


            SaveCurrentState();
        }
    }

    private bool CurrentlyOnGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(0.8f, 0.01f), 0, Vector3.down, 0.5f);
        return hit.collider != null;
    }

    bool platformMode;
    
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
        gameObject.layer = 0;
        platformMode = true;
        animator.SetBool("Platform", true);
        GetComponent<Rigidbody2D>().excludeLayers = LayerMask.GetMask("Nothing");
    }
    public void StopDance()
    {
        gameObject.layer = LayerMask.NameToLayer("Worm");
        GetComponent<Rigidbody2D>().excludeLayers = LayerMask.GetMask("Worm");
        platformMode = false;
        animator.SetBool("Platform", false);
    }
}
