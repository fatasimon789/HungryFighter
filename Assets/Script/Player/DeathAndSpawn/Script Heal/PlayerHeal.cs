using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeal : MonoBehaviour
{
    [Header("Refrece")]
    //private DamagesController _DamagesController;
    private KnockBack knockback;
    public GameObject _impactFlash;
    private Rigidbody2D rgb2;
    
    
    [Header("Effect")]
    public GameObject deathExplotion;
    
    [Header("Heal")]
    public int PlayhealNum;
    
    public Image[] hearts;
    [Header("Position")]
    private Spawn spawnPos;
    
    [Header("Animation")]
    
    public Animator FadeAnimation;
   
    private bool canHitPlayer = true; 
    
    private void Start()
    {
        spawnPos = GetComponent<Spawn>();
        knockback = GetComponent<KnockBack>();
        rgb2 = GetComponent<Rigidbody2D>();
       
        
        updateHealth();
    }
    public void updateHealth() 
    {
        for(int i = 0; i < hearts.Length; i++) 
        {
            if (i < PlayhealNum) 
            {
                hearts[i].color= Color.red;
            }
            else 
            {
                hearts[i].color = Color.black;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (PlayhealNum <= 0)
        {
            die();
        }
    }
    public void EnemyDamages(int  Damages, Vector2 Direction) 
    {
        if (canHitPlayer )
        {
           
            PlayhealNum -= Damages;
            Instantiate(_impactFlash,transform.position,Quaternion.identity);
            updateHealth();
            if ( PlayhealNum > 0) 
            {
                knockback.callKnockBack(Direction, Vector2.up, Input.GetAxisRaw("Horizontal"));
            }
                StartCoroutine(invulnearabilityPlayer()); 
           canHitPlayer = false;
        } 
    }
    public void DeathZoneDamages(int Damages) 
    {
        PlayhealNum -= Damages;
        Instantiate(_impactFlash, transform.position, Quaternion.identity);
        updateHealth();
    }
   
    public void die() 
    {
        //aniamtion die
       
        rgb2.gravityScale = 0f;
        rgb2.velocity = Vector2.zero;
        //   GetComponent<Collider2D>().enabled = false;
        GetComponent<PlayerControler>().enabled = false;
        GetComponent<MeleeAtackManager>().enabled = false;
        StartCoroutine(dieExplotion());

      

    }
    private IEnumerator invulnearabilityPlayer() 
    {
      yield return new WaitForSeconds(1);
        canHitPlayer = true;

    }
    private IEnumerator dieExplotion()
    {
     
       
        yield return new WaitForSeconds(0.75f);
        Instantiate(deathExplotion, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.75f);
        FadeAnimation.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        spawnPos.Respawn();
        FadeAnimation.SetTrigger("FadeIn");
       
    }
  
    
    public void AfterDeath() 
    {

        // GetComponent<Collider2D>().enabled = true;
        rgb2.gravityScale = 5f;
        GetComponent<PlayerControler>().enabled = true;
        GetComponent<MeleeAtackManager>().enabled = true;
        

        PlayhealNum = hearts.Length;
        updateHealth();
    }
}
