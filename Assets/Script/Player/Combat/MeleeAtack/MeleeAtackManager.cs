using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAtackManager : MonoBehaviour
{
    [Header("index")]
    public float defaulForce = 300f;
    public float upwardsForce = 600f;
    public float movementTime = 0.2f;
    [Header("Refrence")]
    private Charactercontroller2d controler;
    private Animator animator;
    //animation of prefaps
    private Animator meleeAnimator;
    
    private bool meleeAtack;
  

    private void Start()
    {
        // the animator of player
        animator = GetComponent<Animator>();
        controler= GetComponent<Charactercontroller2d>();
        //the animator on prefaps
     meleeAnimator = GetComponentInChildren<MeleeAtackWeapon>().gameObject.GetComponent<Animator>();
    }
   
    private void Update()
    {
        CheckInput();
    }
    private void CheckInput() 
    {
      if(Input.GetKeyDown(KeyCode.J)) 
        {
         meleeAtack = true;
        }
        else 
        {
            meleeAtack = false;
        } 
      if(meleeAtack && Input.GetAxis("Vertical")>0) 
        {
            //Player Animation Strike
            meleeAnimator.SetTrigger("UpStrike");
        }
      if(meleeAtack && Input.GetAxis("Vertical")<0 && !controler.m_Grounded) 
        {
            //Player Animation Strike
            meleeAnimator.SetTrigger("DownStrike");

        }
        if (meleeAtack && Input.GetAxis("Vertical") == 0 && controler.m_Grounded)
        {
            //Player Animation Strike
            meleeAnimator.SetTrigger("Atack");
        }
        if (meleeAtack && Input.GetAxis("Vertical") == 0 && !controler.m_Grounded)
        {
            //Player Animation Strike
            meleeAnimator.SetTrigger("Atack");

        }

    }
}
