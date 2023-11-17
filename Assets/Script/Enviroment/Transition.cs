using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [Header("Refrence")]
    [SerializeField] private  Transform [] StartPoint;
    [SerializeField] private Animator Fade;
    [SerializeField] public MeleeAtackManager _meeleeAttackManager;

    private bool Entry = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.name =="Player" && Entry)
            {  
                StartCoroutine(EffectEntry());
               
            }
            else if (collision.gameObject.name == "Player" && !Entry)
            {
                StartCoroutine(EffectExit());
            }
    }
    
    IEnumerator EffectEntry() 
    {
        PlayerControler.Instance.enabled = false;
        _meeleeAttackManager.enabled = false;
        Fade.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        Charactercontroller2d.Instance.transform.position = StartPoint[0].position;
           Entry = false;
        yield return new WaitForSeconds(1f);
        Fade.SetTrigger("FadeIn");
        PlayerControler.Instance.enabled = true;
        _meeleeAttackManager.enabled = true;

    }
    IEnumerator EffectExit()
    {
        PlayerControler.Instance.enabled = false;
        _meeleeAttackManager.enabled = false;
        Fade.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        Charactercontroller2d.Instance.transform.position = StartPoint[1].position;
        Entry = true;
        yield return new WaitForSeconds(1f);
        Fade.SetTrigger("FadeIn");
        PlayerControler.Instance.enabled = true;
        _meeleeAttackManager.enabled = true;

    }
}
