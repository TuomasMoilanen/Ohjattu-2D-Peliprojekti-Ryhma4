using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float moveInput;

    // public float fallMultiplier = 2.5f;
    // public float lowJumpMultiplier = 2f;

    private bool playerFacingRight = true;
    private bool playerJumping = false;
    private bool playerGrounded = false;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask ground;

    public Animator playerAnimator;
    public Rigidbody2D playerRigidbody2D;



    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        playerGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground);
        moveInput = Input.GetAxis("Horizontal");

        if (playerGrounded)
        {
            playerAnimator.SetFloat("Velocity", Mathf.Abs(moveInput));
        }
        if (Input.GetButtonDown("Jump") && playerGrounded)
        {
            playerJumping = true;
            playerAnimator.SetBool("Jump", true);
        }
        else if (Input.GetButtonDown("Jump") && playerGrounded)
        {
            playerAnimator.SetBool("Jump", false);
        }
        /* Placeholder
         
        if (Input.(KeyCode.) && playerGrounded)
        {
            playerAnimator.SetBool("", true);
        }
        else if (Input.(KeyCode.))
        {
            playerAnimator.SetBool("", false);
        }
        */
    }

    void PlayerFlip()
    {
        playerFacingRight = !playerFacingRight;

        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;

        transform.localScale = playerScale;
    }

    private void FixedUpdate()
    {
        playerRigidbody2D.velocity = new Vector2(moveInput * moveSpeed, playerRigidbody2D.velocity.y);

        if (playerFacingRight == false && moveInput > 0)
        {
            PlayerFlip();
        }
        else if (playerFacingRight == true && moveInput < 0)
        {
            PlayerFlip();
        }
        if (playerJumping)
        {
            playerRigidbody2D.AddForce(new Vector2(0f, jumpForce));

            playerJumping = false;
        }

    }
}
