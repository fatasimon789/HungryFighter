using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBoss : State
{
    public float speedBoss = 40f;
    private bool StartingChasing;
    public RunBoss(EnemyBossFalseKnight enemy, StateMachine bossStateMachine) : base(enemy, bossStateMachine)
    {
    }
    public override void AnimationTriggerEvent(EnemyBossFalseKnight.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        switch(triggerType) 
        {
            case EnemyBossFalseKnight.AnimationTriggerType.runAniticipate:
                EventAniticipateRun();
                break;
            case EnemyBossFalseKnight.AnimationTriggerType.running:
                EventRuning();
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
        // if player range <= 25  the boss will stop chasing and  change to atack state
        if (_enemy.distancePlayerBoss < 25) 
        {
            StartingChasing = false;
            _enemy.animator.SetBool("Running", false);
            _enemy._StateMachine.ChangeState(_enemy._atackboss);
        }
        if (StartingChasing) 
        {
            // boss running to player
            _enemy.flip();
            _enemy.animator.SetBool("Running", true);
            Vector2 Target = new Vector2(_enemy.PlayerPos.position.x, _enemy.EnemyPos.position.y);
            Vector2 NewBossMove = Vector2.MoveTowards(_enemy.EnemyPos.position, Target, speedBoss * Time.deltaTime);
            _enemy.RB.MovePosition(NewBossMove);
        }
        // the boss will chasing when  go near 25f
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        
    }
    private void EventAniticipateRun() 
    {
        // if player is to far boss will readdy and running
            _enemy.animator.SetTrigger("RunAni");
        Debug.Log("readdy");
    }
    private void EventRuning()
    {
        StartingChasing = true;
        Debug.Log("run");
    }
}
