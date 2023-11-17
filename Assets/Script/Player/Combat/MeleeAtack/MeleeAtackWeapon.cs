using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAtackWeapon : MonoBehaviour
{  //damage MeleeAtack
    private int damagesAmount = 20;
   // Refence  cac variable can thiet de tao ra dieu kien ...
    private Charactercontroller2d charactercontroller;
    
  // refence rigidbody
    private Rigidbody2D rigidbody2;
  // refence  manger ve amount force khi colider
    private MeleeAtackManager meleeAtackManager;
    // Huong vector khi dat duoc 1 condition
    private Vector2 direction;
    // Se call khi co true colider or false colider
    private bool colider;
    // se Call khi Co true chem xuong' hoac ko;
    private bool downwardStrike;
    private bool ResetForces;
    public void Start()
    {
        charactercontroller = GetComponentInParent<Charactercontroller2d>();
        rigidbody2 = GetComponentInParent<Rigidbody2D>();
        meleeAtackManager = GetComponentInParent<MeleeAtackManager>();
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }
    // tất cả vật va chạm từ collider 2d thì sẽ chạy code  tướng tác
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // condition if colider on Object have Enemy Heal then HandleCollision will run  this conidtion .
        if (collision.GetComponent<EnemyHealth>()) // collision.GetComponent<EnemyBossFalseKnight>()
        {
            HandleCollision(collision.GetComponent<EnemyHealth>());// ,collision.GetComponent<EnemyBossFalseKnight>() 
        }
        if(collision.GetComponent<EnemyBossFalseKnight>()) 
        {
           HandleColssionBossFalseKnight(collision.GetComponent<EnemyBossFalseKnight>());
        }
    }
    private void HandleColssionBossFalseKnight(EnemyBossFalseKnight BossHealth) 
    {
        BossHealth.Damage(damagesAmount);
    }
    private void HandleCollision(EnemyHealth objHealth) //, EnemyBossFalseKnight bossHealth  
    {
        //if EnemyHealscript nhận chém từ player   và  input < 0 nghia la chem luon -1 va k o tren dat
       if(objHealth.giveUpwardForce &&Input.GetAxis("Vertical")<0 && !charactercontroller.m_Grounded) 
        {
            // Huong Vector = 0 1 tuc la huong len
            // chem xuong se true
            // va cham. chem' se true
            direction = Vector2.up;
            downwardStrike = true;
            colider = true;
        }
       // if Player Strike up > 0 and k o duoi dat'
        if (Input.GetAxis("Vertical") > 0 && !charactercontroller.m_Grounded) 
        {
            //huong Vector = 0 -1 tuc la xuong duoi
            // va cham = true;
            direction = Vector2.down;
            colider = true;
        }
        // Player chem' nam giua hoac duoi tuc la và ở trên mặt dất  hoac la = 0
        if ((Input.GetAxis("Vertical") <= 0 && charactercontroller.m_Grounded) 
           || Input.GetAxis("Vertical") == 0 )
        {
            // Player chem' o phia ben  mat phai nhan vat thi huong' Vector se vang ve ben trai tuc la -1 0 
            //va nguoc lai 1 0 ve phia ben phai
           if(charactercontroller.m_FacingRight) 
            {
                direction = Vector2.left;
            }
            else 
            {
                direction = Vector2.right;
            }
            colider = true; 
        }
        // goi la Method Damge tu obhHealth va bien la so mau
        objHealth.Damage(damagesAmount);
        StartCoroutine(NoLongerCollider());
    
    }
    // Method HandleMovement khi dat codition va cham true thi no se co 2 phan condition nho~
    //     neu chem va cham tu tren xuong thi se tao 1 luc huong len tren co luc. dc dieu chinh trong meeleeManager
    //     neu chem va cham tu ngang binh thuong thi se co 1 luc  day lui` nhe dc dieu chinh trong MeleeManager
    private void HandleMovement() 
    {
        if (colider) 
        {
          if(downwardStrike) 
            {
                rigidbody2.AddForce(direction * meleeAtackManager.upwardsForce);
                ResetForces = true;
                
                if (ResetForces) 
                {
                   rigidbody2.velocity = Vector2.up * Time.deltaTime*meleeAtackManager.upwardsForce;    
                }
               
            }
          else 
            {
                rigidbody2.AddForce(direction * meleeAtackManager.defaulForce);
            }
        }
    }
    // Tao delay  cho moi cu chem
    // trong vong 1f ( movementTime) va dong thoi colider = false doward strike = false;
    private IEnumerator NoLongerCollider() 
    {
        yield return new WaitForSeconds(meleeAtackManager.movementTime);

        colider= false;

        downwardStrike=false;
    }
}
