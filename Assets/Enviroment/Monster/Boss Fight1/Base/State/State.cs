using System.Collections;
using UnityEngine;

public abstract class State
    {
    protected EnemyBossFalseKnight _enemy;
    protected StateMachine _Machine;

    public State (EnemyBossFalseKnight enemy,StateMachine bossStateMachine) 
    {
        this._enemy = enemy;
        this._Machine= bossStateMachine;
       
    }
    // make a virtual method and all the individual state will overider all the method ( its look like a real method by Enemy Script)
        public virtual void EnterState() 
        {

        }
        public virtual void ExitState() 
        {
    
        }
        public virtual void FrameUpdate() 
        {
    
        }
        public virtual void PhysicUpdate() 
        {
    
        }
        public virtual void AnimationTriggerEvent(EnemyBossFalseKnight.AnimationTriggerType triggerType) 
        {
    
        }
}
    
