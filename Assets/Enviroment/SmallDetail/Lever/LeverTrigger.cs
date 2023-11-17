using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    public Gate gate;
    bool isGateOpen = false;
    public bool isTriggerOn { get; private set; } 
    public Animator animator; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MeleeWeapon")&& gameObject.tag== "TriggerGate"&& isGateOpen == false) 
        {
           gate.OpenGate();
            animator.SetTrigger("Active");
           isGateOpen = true;
        }

        if (collision.CompareTag("MeleeWeapon") && gameObject.tag == "TriggerUnlockDoor" && !isTriggerOn)
        {
            animator.SetTrigger("Active");
            isTriggerOn = true;
        }
    }
}
