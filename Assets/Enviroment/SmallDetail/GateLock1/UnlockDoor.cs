using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    private Animator animatorUnlock;

    public LeverTrigger[] levelTrigger;

    private BoxCollider2D boxColider2d;


    private void Start()
    {
        animatorUnlock = GetComponent<Animator>();
        boxColider2d= GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        ActiveDoor();
    }

    void ActiveDoor() 
    {
        // if all triger adctive
        for(int i = 0; i < levelTrigger.Length; i++) 
        {
           if(levelTrigger[i].isTriggerOn == true ) 
            {
                boxColider2d.enabled = false;
                animatorUnlock.SetTrigger("ActiveDoor");
            }  
        }
    }
}
