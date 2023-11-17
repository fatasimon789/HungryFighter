using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    // Start is called before the first frame update
    
    Vector3 gateOpenPos;
    Vector3 gateClosePos;

    public float GateIndexClose = 22f;
    public float doorSpeed = 13f;
    private void Start()
    {
        gateOpenPos =  new Vector2 (transform.position.x, transform.position.y + GateIndexClose);
        gateClosePos = new Vector2 (transform.position.x, transform.position.y - GateIndexClose);
    }
    private void Update()
    {

      //  if(isGateClose) 
        //{
          //  OpenGate();
           // kill boss
           //  make isgateclose = true 
        //}
    }
    public void CloseGate() 
    {
        if(transform.position != gateClosePos) 
        {
            transform.position = Vector2.MoveTowards(transform.position, gateClosePos, doorSpeed *2.5f);
        }
    }
    public void OpenGate() 
    {
        if (transform.position != gateOpenPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, gateOpenPos, doorSpeed *2.5f);
        }
    }
}
