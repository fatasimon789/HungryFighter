using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;

public class MiniMonster1 : MonoBehaviour
{
    [Header("index")]
    public float speed;
    private float circleRadius = .2f;
    [Header("Refrence")]
    public GameObject groundcheck;
    private Rigidbody2D EnemyRb2D;
    public LayerMask groundLayer;
    private Animator animator;
    private EnemyHealth enemyHeal;
    
    public bool isFacingRight;
    public bool isGrounded;
    private bool isDead = false;
    
    private void Start()
    {
        EnemyRb2D= GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
        enemyHeal= GetComponent<EnemyHealth>();
    }
    private void Update()
    {
        if (!isDead)
        {
            EnemyRb2D.velocity = new Vector2(-1 * speed * Time.deltaTime, 0);
            isGrounded = Physics2D.OverlapCircle(groundcheck.transform.position, circleRadius, groundLayer);

            if (!isGrounded && isFacingRight)
            {
                Flip();
            }
            else if (!isGrounded && !isFacingRight)
            {
                Flip();
            }
        }
        DeathMonster1();
    }
    void Flip() 
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(new Vector2(0,180));
        speed = -speed;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundcheck.transform.position, circleRadius);
    }
    void DeathMonster1() 
    {
        if (enemyHeal.currentHealth <= 0) 
        {
           StartCoroutine(DeathAnimation());
        }
    }
    private IEnumerator DeathAnimation() 
    {
        isDead = true;
        EnemyRb2D.velocity = Vector2.zero;
        animator.SetTrigger("DeathMonster1");
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    } 
}
