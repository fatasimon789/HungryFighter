using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class MiniMonsterDamages : MonoBehaviour
{
    public GameObject Player;
    public LayerMask PlayerLayerMark;
    public Vector3 AttackOffset;

    public float radius ;

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 Pos = transform.position;
        Pos += transform.right * AttackOffset.x;
        Pos += transform.up * AttackOffset.y;
        Collider2D ColiderBehind = Physics2D.OverlapCircle(Pos, radius, PlayerLayerMark);
        if (ColiderBehind != null) 
        {
            collision.GetComponent<PlayerHeal>().EnemyDamages(1, transform.right);
            
        }
        else if (collision.CompareTag("Player") )
        {
            collision.GetComponent<PlayerHeal>().EnemyDamages(1, -transform.right);
        }
      
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * AttackOffset.x;
        pos += transform.up * AttackOffset.y;
        Gizmos.DrawWireSphere(pos, radius);
    }
}
