using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float maxHealth = 100f;
    private float currentHealth;
    private float jumpForce;
    [SerializeField]
    private Rigidbody2D rb;
    private bool grounded;
    private Animator animator;

    // Better jump with four lines of code video (4L)
    private float fallMultiplier = 2.5f; // Gravity when the character's falling down
    private float lowJumpMultiplier = 2f; // Gravity when doing a low jump

    //Attack variables
    public Transform attackPoint;
    [SerializeField]
    public float attackRange = 4f;
    [SerializeField]
    public float attackDamage = 100f;
    public float attackRate = 0.35f;
    public float nextAttackTime = 0f;
    [SerializeField]
    public LayerMask enemyLayer;

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
        if (Input.GetButtonDown("Jump") && grounded == true)
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

        // Attacking
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }

        }
    }

    void GroundCheck()
    {
        Vector3 checkPosition = transform.position - new Vector3(0, transform.localScale.y / 2, 0);
        RaycastHit2D castHit = Physics2D.BoxCast(checkPosition, new Vector2(1.2f, 0.3f), 0, Vector2.zero, 0, LayerMask.GetMask("Ground"));
        if (castHit && rb.velocity.y <= 0)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);
            enemy.GetComponent<EnemyScript>().TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Death();
        }
    }
    void Death()
    {
        Debug.Log("You died!");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
