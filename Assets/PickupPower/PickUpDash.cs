using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDash : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            Pickup(collision);
        }
    }
    void Pickup(Collider2D player) 
    {
        // The effect when pick up
        // Effect power up
        DashPlayer _dashplayer = player.GetComponent<DashPlayer>();
        _dashplayer.enabled= true;
        
        Destroy(gameObject);
    }
   
}
