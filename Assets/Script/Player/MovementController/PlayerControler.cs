using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    protected PlayerControler character;

    [Header("Refrence")]
    public Charactercontroller2d controller;
    public Animator animator;
    private KnockBack knockBack;

    private float Horizontal = 1f;
    bool jump = false;
    [Header("SpeedMovement")]
    public float speed = 20f;
    private void Start()
    {
        knockBack= GetComponent<KnockBack>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    public static PlayerControler Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Update()
    {
        if (!knockBack.IsBeingKnockBack)
        {
            Horizontal = Input.GetAxisRaw("Horizontal") * speed;
            animator.SetFloat("Speed", Mathf.Abs(Horizontal));
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("Jumping", true);
            }
        }
    }
    public void Onlanding() 
    {
        animator.SetBool("Jumping", false); 
    }
    private void FixedUpdate()
    {
        controller.Move(Horizontal*Time.deltaTime, false, jump);
        
        jump = false;
        
    }
    //protected virtual void Intilization() 
    //{
      //controller = GetComponent<Charactercontroller2d>();
      //character  = GetComponent<PlayerControler>();
      //animator = GetComponent<Animator>();
      //controller.m_CrouchDisableCollider = GetComponent<Collider2D>();
    //}
}
