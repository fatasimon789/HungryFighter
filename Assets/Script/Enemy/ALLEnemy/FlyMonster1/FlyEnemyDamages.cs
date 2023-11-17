using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyDamages : MonoBehaviour
{
    public GameObject Player;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (transform.position.x >= Player.transform.position.x)
            {
                collision.GetComponent<PlayerHeal>().EnemyDamages(1, -transform.right);
            }
            else
            {
                collision.GetComponent<PlayerHeal>().EnemyDamages(1, transform.right);
            }
        }
     }    
  
}
