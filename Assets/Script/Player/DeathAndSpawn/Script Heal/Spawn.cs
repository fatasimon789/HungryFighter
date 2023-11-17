using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [Header ("Health")]
    private PlayerHeal playerHeal;
    [Header("Postion")]
    private Transform currentCheckPoint;
    private Vector2 currentPos;
    //  [Header("Components")]
    // private Behaviour[] components;
    

    private void Awake()
    {
        playerHeal= GetComponent<PlayerHeal>();
        currentPos= transform.position;
        
    }
    public void Respawn() 
    {
        if (currentCheckPoint == null)
        {
            transform.position = currentPos;
        }
        else
        {
            transform.position = currentCheckPoint.position;
        } 
       
        playerHeal.AfterDeath();
     
    }
   private void OnTriggerEnter2D(Collider2D collision) 
    {
     if(collision.transform.tag == "CheckPoint") 
        {
            currentCheckPoint = collision.transform;
         //   collision.GetComponent<Collider>().enabled= false;
            Debug.Log("i have them");

        }
    }
}
