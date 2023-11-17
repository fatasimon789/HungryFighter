using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGate : MonoBehaviour
{
    public Gate[] Gate;
    bool isGateClose;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& !isGateClose) 
        {
            for (int i = 0; i < Gate.Length; i++)
            {
                Gate[i].CloseGate();
            }
            isGateClose = true;
        }
    }
}
