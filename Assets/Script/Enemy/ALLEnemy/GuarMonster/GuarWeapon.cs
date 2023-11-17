using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GuarWeapon : MonoBehaviour
{
    [Header("Refrence")]
    public PlayerHeal _playerHeal;
    public KnockBack _knockback;
    
    [Header("Index")]
    public int attackDamages = 1;
    public float AttackRange;

    private bool inmortal = false;
    private bool canKnockback=true;
    public Vector3 AttackOffset;
    public LayerMask attackMask;
    
    public void Atack() 
    {
         Vector3 Pos = transform.position;
        Pos += transform.right * AttackOffset.x;
        Pos += transform.up * AttackOffset.y;
           Collider2D colliderInfo = Physics2D.OverlapCircle(Pos, AttackRange, attackMask);
        StartCoroutine(invulnearabilityPlayerWeapon()); 
        if(colliderInfo != null&& !inmortal && canKnockback) 
         {
            _playerHeal.EnemyDamages(1,-transform.right);
            inmortal = true;
            canKnockback = false;
        } 
    }
    private IEnumerator invulnearabilityPlayerWeapon() 
    {
      yield return new WaitForSeconds(1);
       inmortal = false;
        canKnockback = true;
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * AttackOffset.x;
        pos += transform.up * AttackOffset.y;
        Gizmos.DrawWireSphere(pos, AttackRange);
    }
}
