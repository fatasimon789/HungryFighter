using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonsterBomb : MonoBehaviour
{
    private EnemyHealth enemyHeal;

    private void Start()
    {
        enemyHeal = GetComponent<EnemyHealth>();
    }
    private void Update()
    {
        DeathMonsterBomb();
    }
    void DeathMonsterBomb()
    {
        if (enemyHeal.currentHealth <= 0)
        {
            StartCoroutine(DeathAnimation());
        }
    }
    private IEnumerator DeathAnimation() 
    {
        //animation dead  
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    } 
}
