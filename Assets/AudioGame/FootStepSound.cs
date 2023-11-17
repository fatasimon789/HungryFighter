using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    public AudioSource footstep;
    private float horizontal;

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        Debug.Log(horizontal);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && horizontal !=0)
        {
            footstep.enabled = true;
        }
        if(collision.gameObject.tag == "Ground" && horizontal == 0) 
        {
                footstep.enabled = false;
        }
    }
}
