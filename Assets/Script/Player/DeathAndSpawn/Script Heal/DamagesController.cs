using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagesController : MonoBehaviour
{
    [Header("Refrence")]
    public PlayerHeal playerHeal;
    public KnockBack knockback;
    
    [Header("Damages")]
    public int GetDamages;
    
    private bool CanHitPlayer = true ;
    private bool PlayerGetDamages = true;
    [SerializeField]
    private float invulnerabilitytimePlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& gameObject.tag == "Monster") 
        {
         //   Damages();
            StartCoroutine(invulnearabilityPlayer());
        }
        if (collision.CompareTag("Player") && gameObject.tag == "MonsterBomb")
        {
            DamagesBomb();
            StartCoroutine(invulnearabilityPlayer());
        }
        if(collision.CompareTag("Player")&& gameObject.tag == "DeathZone") 
        {
            DamagesDeathZone();
        }
        if(collision.CompareTag("Player")&& gameObject.tag == "MonsterGuar") 
        {
            DamagesGuard();
        }
    }
    private IEnumerator invulnearabilityPlayer() 
    {
        yield return new WaitForSeconds(invulnerabilitytimePlayer);
        CanHitPlayer = true;
       
    }
    /*    void Damages() 
    {
        if ( PlayerGetDamages && CanHitPlayer)
        {
            playerHeal.PlayhealNum = playerHeal.PlayhealNum - GetDamages;
            playerHeal.updateHealth();
            CanHitPlayer = false;
           // knockback.callKnockBack(transform.right, Vector2.up, Input.GetAxisRaw("Horizontal"));
        }
    }*/
      void DamagesBomb()
    {
        if (CanHitPlayer && PlayerGetDamages )
        {
            playerHeal.PlayhealNum = playerHeal.PlayhealNum - GetDamages;
            playerHeal.updateHealth();
            CanHitPlayer = false;
            //knockback.callKnockBack(transform.right, Vector2.up, Input.GetAxisRaw("Horizontal"));
            // gameObject.SetActive(false);
            Destroy(gameObject);
           
        }
     }
     public void DamagesGuard() 
    {
      if(CanHitPlayer && PlayerGetDamages ) 
        {
            playerHeal.PlayhealNum =playerHeal.PlayhealNum - GetDamages;
            playerHeal.updateHealth();
            CanHitPlayer = false;
            knockback.callKnockBack(transform.right, Vector2.up, Input.GetAxisRaw("Horizontal"));
        }
    }
     void DamagesDeathZone() 
    {
      if(!CanHitPlayer && PlayerGetDamages) 
        {
            playerHeal.PlayhealNum = playerHeal.PlayhealNum - GetDamages;
            playerHeal.updateHealth();
            CanHitPlayer = true;
        }
    }
}
