using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowerPlayer : MonoBehaviour
{
    [Header("Refrence")]
    [SerializeField] private Transform _PlayerTransform;

    [Header("Flip Rotation Stats")]
    [SerializeField] private float _FlipRotationTime=0.5f;

   // private Coroutine _turnCoroutine;

    private Charactercontroller2d Player;

    private bool isFacingRight;

    private void Awake()
    {
         Player =  _PlayerTransform.GetComponent<Charactercontroller2d>();

        isFacingRight = Player.m_FacingRight;
    }
    private void Update()
    {
        transform.position= _PlayerTransform.position;
    }
#region  make camera moving after player flip
    public void callTurn() 
    {
         StartCoroutine(FlipYlerp());
    } 
    private IEnumerator FlipYlerp() 
    {
     float startRotation = transform.localEulerAngles.y;
        
     float endRotationAmount = DetermineEndRotation();
       
     float yRotation ;
        float elapsedTime = 0f;
        while (elapsedTime < _FlipRotationTime) 
        {
            elapsedTime = Time.deltaTime;
           // make the cemera moving from startRotation to endration on time.deltime by elapstime
            yRotation = Mathf.Lerp(startRotation,endRotationAmount, (elapsedTime / _FlipRotationTime));
          
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
            yield return null;
        }
    }
    private float DetermineEndRotation() 
    {
      isFacingRight = !isFacingRight;
      if(isFacingRight) 
        {
            return 180f;
        }
       else
        {
            return 0f;
        }
    }
#endregion     
}
