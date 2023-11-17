using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            trigger.gameObject.GetComponent<PlayerHitEffect>().Stoptime(0.05f, 10, 1f);
        }
    }
}
