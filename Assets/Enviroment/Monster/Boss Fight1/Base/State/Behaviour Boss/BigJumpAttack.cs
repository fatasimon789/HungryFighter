using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class BigJumpAttack : State
{
    private float finalJumpForce= 80;
    private float horizontalFinalJump = 30f;
    private float CountingJumpFinal;
    private float shockWaveSpeed = 30f;
    private float  direction;
    Rigidbody2D ShockWave;
    public BigJumpAttack(EnemyBossFalseKnight enemy, StateMachine bossStateMachine) : base(enemy, bossStateMachine)
    {
        
    }
    public override void AnimationTriggerEvent(EnemyBossFalseKnight.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        switch(triggerType) 
        {
            case EnemyBossFalseKnight.AnimationTriggerType.JumpFinalAttack1:
                EventjumpFinalAttack();
                break;
            case EnemyBossFalseKnight.AnimationTriggerType.JumpFinalAttack2:
                EventjumpFinalAttackGround();
                break;
            case EnemyBossFalseKnight.AnimationTriggerType.JumpFinalAttack3:
                EventjumpFinalAttackRecover();
                break;
            case EnemyBossFalseKnight.AnimationTriggerType.Idle2:
                EventBackToIdle3();
                break;
            case EnemyBossFalseKnight.AnimationTriggerType.JumpFinalRecover:
                EventEndAttackRecover();
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
        if (_enemy.ShockWave.transform.position.x == _enemy.PlayerPos.position.x) 
        {
          // delete the shock wave; 
        } 
        
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
    private void EventjumpFinalAttack() 
    {
        _enemy.animator.SetTrigger("JumpFinalAttack");
        _enemy.flip();
        if (_enemy.PlayerPos.position.x < _enemy.EnemyPos.position.x)
        {
            direction = -1f;
        }
        else
        {
            direction = 1f;
        }
        _enemy.RB.AddForce(new Vector2(-horizontalFinalJump*direction, finalJumpForce), ForceMode2D.Impulse);
        CountingJumpFinal += 1;
    }
    private void EventjumpFinalAttackGround()
    {
       float distance = _enemy.EnemyPos.transform.position.y - _enemy.GroundLandingPos.transform.position.y;
        if (_enemy.GroundLanding && distance < 40f)
        {
            _enemy.animator.SetTrigger("JumpFinalAttack1");
            //instance shock wave;  
             ShockWave = GameObject.Instantiate(_enemy.ShockWavePrefap, _enemy.ShockWavePos.position, _enemy.ShockWave.transform.rotation);
            ShockWave.velocity = new Vector2(direction * shockWaveSpeed, 0);
        }
    }
    private void EventjumpFinalAttackRecover()
    {
        _enemy.animator.SetTrigger("JumpingFinalAttack2");
    }
    private void EventEndAttackRecover() 
    {
        _enemy.animator.SetTrigger("JumpingFinalAttack3");
    }
    private void EventBackToIdle3()
    {
        _enemy.animator.SetTrigger("Idle");
        if (CountingJumpFinal == 1) 
        {
            _enemy._StateMachine.ChangeState(_enemy._jumpBoss);
            CountingJumpFinal = 0;
        }
    }
}
