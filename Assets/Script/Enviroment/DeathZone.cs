using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public PlayerHeal _playHeal;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
       {
            collision.GetComponent<PlayerHeal>().DeathZoneDamages(100); 
       }
    }
}
