using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Timeline;
using Debug = UnityEngine.Debug;

public class EnemyScript : MonoBehaviour
{
    private Animator animator;
    public float maxHealth = 150f;
    public float currentHealth;
    [SerializeField]
    private GameObject thisEnemy;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private LayerMask playerLayer;
    private float attackDamage = 40f;
    [SerializeField]
    private float attackRate = 0.35f;
    [SerializeField]
    private float nextAttackTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Implement AI
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
            animator.SetTrigger("Death");
            Death();
        }
    }

    void Death()
    {
        Debug.Log("Dead");
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<BoxCollider2D>());
        animator.SetTrigger("Death");
        Destroy(thisEnemy, 2);
    }

    void Attack()
    {
        Debug.Log("Attacking!");
        animator.SetTrigger("Attack");
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach (Collider2D player in hitPlayers)
        {
            Debug.Log("Hit Player!");
            player.GetComponent<PlayerController>().TakeDamage(attackDamage);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
