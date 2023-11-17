using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float KnockBackTime = 0.2f;
    public float hitDirectionForce = 10f;
    public float constForce = 5f;
    public float inputForce = 7.5f;

    public bool IsBeingKnockBack;

    private Rigidbody2D rgb2;
    private Coroutine knockBackCoroutine;
   

    private void Start()
    {
        rgb2= GetComponent<Rigidbody2D>();
    }
    public IEnumerator KnockBackAction(Vector2 HitDirection,Vector2 constantForceDirection,float inputDirection) 
    {
      IsBeingKnockBack= true;
      
        // lực chạm
        Vector2 _hitForce;
        // lực không đổi
        Vector2 _constantForce;
        // lực bị văng ra
        Vector2 _knockBackForce;
        // kết hợp lực
        Vector2 _combinedForce;

        //lực chạm sẽ có  vector nhận vào và lực điều chỉnh có sẵn
        _hitForce = HitDirection * hitDirectionForce;
        // lực không đổi sẽ cũng có vector nhận vào và lực điều chỉnh có sẵn
        _constantForce = constantForceDirection * constForce;

        float elapsedTime = 0f;
        
         while (elapsedTime < KnockBackTime) 
         {
            elapsedTime += Time.fixedDeltaTime;
            // tổng lực văng sẽ = lực chạm + lực không đổi
            _knockBackForce = _hitForce + _constantForce;
            // nếu  hướng nhận vào ko phai là 0 thì
            if(inputDirection != 0 ) 
            {
                // lực kết hợp sẽ bằng  tổng lực văng + 1 vector nhận hướng  và trục y = 0
                _combinedForce = _knockBackForce + new Vector2(inputDirection*inputForce, 0f);
            }
            else 
            {
                // nếu hướng nhận vào = 0 
                // thì lực kết hợp sẽ chỉ bằng lực văng
               _combinedForce= _knockBackForce;
            }
            // aplly lực knockback vào rgb
            rgb2.velocity = _combinedForce;
            yield return new WaitForFixedUpdate();
        }
            yield return new WaitForSeconds(KnockBackTime+0.2f);
            IsBeingKnockBack = false;
    }
       public void callKnockBack(Vector2 HitDirection, Vector2 constantForceDirection, float inputDirection) 
      {
         knockBackCoroutine = StartCoroutine(KnockBackAction(HitDirection,constantForceDirection,inputDirection));
      }  
}
