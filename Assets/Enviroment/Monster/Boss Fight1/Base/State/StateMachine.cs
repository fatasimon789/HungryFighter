using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    // To conect enemy Script to stamachine and state
    public State CurrentState { get;  set; }


  
    public  void InititeNewState(State startingState) 
    {
        CurrentState = startingState;
        CurrentState.EnterState();
    }
    // Change state to next event
    public void ChangeState(State newState) 
    {
      CurrentState.ExitState();
      CurrentState = newState;
      CurrentState.EnterState();
    }   
}
