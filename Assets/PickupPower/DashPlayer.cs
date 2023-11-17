using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPlayer : MonoBehaviour
{
    private DashPlayer dashplayer;
    private Rigidbody2D rgb;
    [SerializeField] private TrailRenderer trailRenderer;

    private bool CanDash = true;
    private bool IsDashing;
    private float DashingForce = 24f;
    private float DashingTime = 0.2f;
   
    private void Start()
    {
       dashplayer= GetComponent<DashPlayer>();
        dashplayer.enabled = false;
        rgb= GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (IsDashing) 
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && CanDash) 
        {
           StartCoroutine(Dash());
        }
        
    }
    private void FixedUpdate()
    {
        if (IsDashing) 
        {
            return;
        }
    }
    private IEnumerator Dash() 
    {
      CanDash = false;
      IsDashing = true;
        float originalGravity = rgb.gravityScale;
        rgb.gravityScale = 0f;
        rgb.velocity = new Vector2(transform.localScale.x * DashingForce, 0f);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(DashingTime);
        trailRenderer.emitting = false;

        rgb.gravityScale = originalGravity;
        IsDashing= false;
        CanDash=true;

    }
 }
