using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private bool Getdamageble = true;
    [SerializeField]
    private int maxHealAmouth;
    [SerializeField]
    private float invulnerabilitytime = 0.5f;
    // khi enemy bi danh thi Player bi vang len tren
    public bool giveUpwardForce = true;
    private bool Gothit = false;
    
    public int currentHealth;
    
    private EnemyHitFlash _Enemyhitflash;

    private void Start()
    {
        currentHealth = maxHealAmouth;
        _Enemyhitflash= GetComponent<EnemyHitFlash>();
      
    }
    public void Damage (int amountHP) 
    {
       if(Getdamageble && !Gothit && currentHealth> 0) 
        {
            Gothit = true;
            currentHealth = currentHealth - amountHP;
         // enemy die 
         if (currentHealth <= 0) 
         {
            currentHealth = 0;
             
         }// enemy alive wait imune 0,1s and hit again
         else 
         {
             StartCoroutine(canHitAgain());
         }
        }
        // damage flash effect
        _Enemyhitflash.CallDamagesFlash();
    }
    private IEnumerator canHitAgain() 
        {
            yield return new WaitForSeconds(invulnerabilitytime);   
            
            Gothit= false;
        }
}
