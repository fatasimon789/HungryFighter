using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GuarMonster : MonoBehaviour
{

    public GameObject Player;
    private Rigidbody2D GuarRgb2d;
    EnemyHealth _enemyheal;
    public Gate[] _gate  ;

    private Animator animator;
    public Animator ChillAnimator;
    public float speedMonsterGuar;
    private float distance;
    public float visionMonster;
    public float AtackRange;

    private bool isFlip = false;
    private bool isChassing=true;
    private bool EnemyDead = false;
    // Start is called before the first frame update
    void Start()
    {
        GuarRgb2d= GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
        _enemyheal= GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyVision();
        EnemyAtackAnimation();
        AnimationEnemyDie();
    }

   private void EnemyVision() 
    {
        if (!EnemyDead)
        {
            distance = Vector2.Distance(transform.position, Player.transform.position);
            if (distance < visionMonster)
            {
                animator.SetTrigger("Wake Up");
                Invoke("EnemyChassing", 1.2f);
                //  EnemyChassing();
            }
            EnemyLookingPlayer();
        }
    }
   private void EnemyChassing() 
    {
        if (isChassing && !EnemyDead) 
        {
            Vector2 Target = new Vector2(Player.transform.position.x, GuarRgb2d.transform.position.y)-new Vector2(5,0);
            Vector2 newEnemyPos = Vector2.MoveTowards(this.transform.position, Target, speedMonsterGuar * Time.deltaTime);
            GuarRgb2d.MovePosition(newEnemyPos);
            animator.SetTrigger("Walk");
        }
   
    }
    private void EnemyLookingPlayer() 
    {
        Vector3 Flips = transform.localScale;
        Flips.z *= -1f;
        if(transform.position.x > Player.transform.position.x && isFlip) 
        {
            transform.localScale= Flips;
            transform.Rotate(0, 180, 0);
            isFlip = false;
          
          
        }
        else if (transform.position.x < Player.transform.position.x && !isFlip) 
        {
            transform.localScale = Flips;
            transform.Rotate(0, 180, 0);
            isFlip = true;
           
        }
    }
    private  IEnumerator StopAndAtack() 
    {
        ChillAnimator.SetTrigger("EnemyAttack");
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1.3f);
        isChassing= true;
    }
    private IEnumerator AnimationDeath() 
    {
        EnemyDead = true;
        GuarRgb2d.velocity = Vector2.zero;
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    
        for(int i = 0; i <_gate.Length; i++) 
        {
            _gate[i].OpenGate(); 
        }

       
    }
   private void EnemyAtackAnimation() 
    {
      if(Vector2.Distance(Player.transform.position,GuarRgb2d.transform.position) <=AtackRange ) 
        {
            isChassing = false;
            StartCoroutine(StopAndAtack());
            Invoke("RessetAtackAnimation",1f);
           
        }
    }
    private void RessetAtackAnimation() 
    {
        animator.ResetTrigger("Attack");
        ChillAnimator.ResetTrigger("EnemyAttack");
    }
    private void AnimationEnemyDie() 
    {
      if(_enemyheal.currentHealth <= 0) 
        {
            StartCoroutine(AnimationDeath());
        }
    }
}
