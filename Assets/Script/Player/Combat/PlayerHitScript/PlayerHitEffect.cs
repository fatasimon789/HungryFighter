using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitEffect : MonoBehaviour
{
    [Header("index")]
    private float Speed;
    //public float Forcehitting = 300f;

    //Vector2 ForceDirectionHit;
    private bool RestoreTime;
    [Header("Refrence")]
   // public GameObject Impactflash;
    private Animator Anim;
    //private Rigidbody2D rgb2;

    void Start()
    {
        // mặc định thời gian khoi phuc time la false
        RestoreTime = false;
        Anim = GetComponent<Animator>();
      //  rgb2 = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        // nếu true sẽ xãy ra 2 option 
        if(RestoreTime) 
        {
            // bị hit time sẽ bị  dưới 1f và nhân vật sẽ bị chậm
            
          if (Time.timeScale < 1f) 
            {
                Time.timeScale+= Time.deltaTime*Speed;
            }
          // trở lại bình thường với speed bình thường thời gian khôi phục lại
            else 
            {
               Time.timeScale= 1f;
                RestoreTime = false;
            }
        }
    }
    public void  Stoptime (float Changetime,int RestoreSpeed,float Delay) 
    {
        Speed = RestoreSpeed; // khoi phuc toc do  la 10 by Hazard

        if (Delay > 0)   // Nap dieu kien delay la 1 
        {
            // stop coroutine delay tuc' la giam tu 1 xuong 0 
            // start courtine delay tuc la tu 0 len 1 lai ( resset lai )
           StopCoroutine(StartTimeAgain(Delay)); 
           StartCoroutine(StartTimeAgain(Delay));
        }
        else 
        {
            //khi coroutine tu  1 xuong 0 se kich hoat  DK khoi phuc time = true
          RestoreTime= true;
        }
        //Instantiate(Impactflash,transform.position,Quaternion.identity); //Tao. clone ve effect 
                                                                         // set animator
        
        Time.timeScale = Changetime;

    }
    IEnumerator StartTimeAgain(float amt) 
    {
        // khoi phuc time la true 
        RestoreTime = true;

        yield return new WaitForSecondsRealtime(amt);
    }
}
