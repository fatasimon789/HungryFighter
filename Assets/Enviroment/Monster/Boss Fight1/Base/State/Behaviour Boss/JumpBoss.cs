using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class JumpBoss : State
{
    public float horizontalForce = 15f;
    public float jumpForce = 60f;
    public float startJumpTime = 0f;
    public float distance;
    public float CountingJump = 0;
    public float speedTest = 20;

    public bool shakeCameraOnLanding;
    public JumpBoss(EnemyBossFalseKnight enemy, StateMachine bossStateMachine) : base(enemy, bossStateMachine)
    {
    }
    public override void AnimationTriggerEvent(EnemyBossFalseKnight.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        switch (triggerType)
        {
            case EnemyBossFalseKnight.AnimationTriggerType.Jump:
                JumpAttack();
                break;
            case EnemyBossFalseKnight.AnimationTriggerType.JumpLanding:
                EventLanding();
                break;
            case EnemyBossFalseKnight.AnimationTriggerType.Idle:
                EventBackToIdle();
                break;
            case EnemyBossFalseKnight.AnimationTriggerType.JumpAnticipate:
                EventAnimationConTroller();
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
    // active when the key frame on Jump Aniticipate running end 1 
    public void JumpAttack()
    {
 
        if (_enemy.GroundCheck)
        {
            float direction;
            if (_enemy.PlayerPos.position.x < _enemy.EnemyPos.position.x)
            {
                direction = -1f;  
            }
            else
            {
                direction = 1f;
            }
             _enemy.RB.AddForce(new Vector2(horizontalForce * direction, jumpForce), ForceMode2D.Impulse);
            _enemy.animator.SetTrigger("Jumping"); //1
        }
    }
    private void EventAnimationConTroller()
    {
            _enemy.animator.SetTrigger("JumpAni"); //0
            _enemy.flip();

    }
    // active when the key frame on Jumping  running end and distance need <30 2 
    private void EventLanding()
    {
        distance = _enemy.EnemyPos.transform.position.y - _enemy.GroundLandingPos.transform.position.y;
        if (_enemy.GroundLanding && distance < 30f)
        {
            _enemy.animator.SetTrigger("Land"); 
        }
    }
    // active when the key frame on Land  running end 3 
    private void EventBackToIdle()
    {
        CountingJump += 1;
        _enemy.animator.SetTrigger("Idle");
        if (CountingJump == 3 && _enemy.distancePlayerBoss <=25) 
        {
            _enemy._StateMachine.ChangeState(_enemy._atackboss);
            CountingJump = 0;
        }
         if (CountingJump == 3 && _enemy.distancePlayerBoss >25)
        {
            _enemy._StateMachine.ChangeState(_enemy._runBoss);
            CountingJump = 0;

        }
    }
}
