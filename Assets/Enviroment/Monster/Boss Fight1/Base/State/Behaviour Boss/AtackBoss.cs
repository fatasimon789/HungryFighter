using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class AtackBoss : State
{
    public float CountingAttack;
    public AtackBoss(EnemyBossFalseKnight enemy, StateMachine bossStateMachine) : base(enemy, bossStateMachine)
    {
    }
    public override void AnimationTriggerEvent(EnemyBossFalseKnight.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        switch(triggerType) 
        {
            case EnemyBossFalseKnight.AnimationTriggerType.AttackAnticipate:
                EventAttackAnicipate();
                break;
            case EnemyBossFalseKnight.AnimationTriggerType.Attack:
                EventAttack();
                break;
            case EnemyBossFalseKnight.AnimationTriggerType.AttackRecover:
                EventAttackCover();
                break;
            case EnemyBossFalseKnight.AnimationTriggerType.Idle1:
                EventBackToIdle1();
                break;
            default:
                break;
        }
    }
    public override void EnterState()
    {
        base.EnterState();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
   private void  EventAttackAnicipate() 
    {
        // if boss range <= 25 then attack
        if (_enemy.distancePlayerBoss <= 25  )
        {
           
            _enemy.animator.SetTrigger("AttackAniticipate");
            // else change to run state to catch player
        }
        else if (_enemy.distancePlayerBoss > 25) 
        {
            _enemy._StateMachine.ChangeState(_enemy._runBoss);
        }
   }
    private void EventAttack()
    {
        CountingAttack += 1;
        _enemy.flip();
            _enemy.animator.SetTrigger("Attack");
    }
    private void EventAttackCover()
    {
            _enemy.animator.SetTrigger("AttackRecover");
   
    }
    private void EventBackToIdle1()
    {
        _enemy.animator.SetTrigger("Idle");
        if (CountingAttack == 3)
        {
            _enemy._StateMachine.ChangeState(_enemy._bigJumpAttack);
            CountingAttack = 0;
        }
    }
}
