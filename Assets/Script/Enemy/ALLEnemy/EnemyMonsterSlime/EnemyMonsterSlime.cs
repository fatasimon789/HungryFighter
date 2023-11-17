using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonsterSlime : MonoBehaviour
{
    private EnemyHealth enemyHeal;

    private void Start()
    {
        enemyHeal = GetComponent<EnemyHealth>();
    }
    private void Update()
    {
        DeathMonsterSlime();
    }
    void DeathMonsterSlime()
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
