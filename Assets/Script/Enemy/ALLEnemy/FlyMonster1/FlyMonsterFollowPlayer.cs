using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMonsterFollowPlayer : MonoBehaviour
{
    [Header ("Refrence")]
    private Transform PlayerPos;
    private Animator animator;
    private Rigidbody2D rgb2;

    private bool isDead;
    public float speed;
    public float RangeAtack;
    private bool isFacingLeft;
    void Start()
    {
        animator= GetComponent<Animator>();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        rgb2 = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        deathEmemy();
        if (!isDead)
        {
            float distanceEnemy = Vector2.Distance(PlayerPos.position, transform.position);
            // if on range this will atack
            if (distanceEnemy < RangeAtack)
            {
                animator.SetBool("Attack1", true);
                transform.position = Vector2.MoveTowards(this.transform.position, PlayerPos.position, speed * Time.deltaTime);
            }
            // or out range if will stop attack
            else
            {
                animator.SetBool("Attack1", false);
            }
            // if pos monster > pos player && now is left
            if(this.transform.position.x >= PlayerPos.transform.position.x && isFacingLeft) 
            {
                flip();
            }
            // if pos monster < pos player && now is right 
            else if (this.transform.position.x <= PlayerPos.transform.position.x && !isFacingLeft)
            {
                flip();
            } 
        }
    }
    void flip() 
    {
        isFacingLeft = !isFacingLeft;
        transform.Rotate(new Vector2(0, 180));
    }
    void deathEmemy() 
    {
      if (GetComponent<EnemyHealth>().currentHealth <= 0) 
        {
            StartCoroutine(DeathAnimation());
        }
    }
    private IEnumerator DeathAnimation()
    {
        isDead = true;
        //rgb2.velocity = Vector2.zero;
        animator.SetTrigger("Dead");
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, RangeAtack);
    }
}
