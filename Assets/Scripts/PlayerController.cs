using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float hitPoints;
    [SerializeField]
    private float jumpForce;
    private Rigidbody2D rb;
    [SerializeField]
    private bool grounded;
    private Animator animator;

    // Better jump with four lines of code video (4L)
    private float fallMultiplier = 2.5f; // Gravity when the character's falling down
    private float lowJumpMultiplier = 2f; // Gravity when doing a low jump

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        transform.Translate(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // 4L gravity application during jumps
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime; // Apply more gravity when downwards velocity is gained
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime; // Apply again more gravity, if there is upwards velocity and the Jump-button is not held down
        }

    }
}
