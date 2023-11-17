using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyBossFalseKnight : MonoBehaviour, IDameable, IEnemyMoveable, ITriggerable
{
    // interface form healBoss
    [field: SerializeField] public float maxHealth { get; set; } = 1000f;
    [field: SerializeField] public GameObject ShockWave;
    public float CurrentHeal { get; set; }
    public Rigidbody2D RB { get; set; }
    public bool isFacingRight { get ; set; }
    #region Refrence Inspector
    public Transform PlayerPos;
    public Transform EnemyPos;
    public LayerMask groundLayer;
    public Transform GroundcheckPos;
    public Transform GroundLandingPos;
    public Transform ShockWavePos;
    public Rigidbody2D ShockWavePrefap;
    #endregion
    #region  Field Inspector hiding

    [HideInInspector] public Animator animator;
    [HideInInspector] public bool GroundCheck ;
    [HideInInspector] public bool GroundLanding;
    [HideInInspector] public float radiusGroundCheck = 5f;
    [HideInInspector] public float raiusGroundLanding = 12f;
    [HideInInspector] public bool isFacingLeft = true;
    [HideInInspector] public float distancePlayerBoss;
    #endregion

    #region State Machine Variables
    public StateMachine _StateMachine { get; set; }
    public AtackBoss _atackboss { get; set; }
    public RunBoss _runBoss { get; set; }
    public JumpBoss _jumpBoss { get; set; }
    public BigJumpAttack _bigJumpAttack { get; set; }
    public bool IsAgroed { get; set; }
    public bool IsWithStrikingDistance { get; set; }
    #endregion
    private void Awake()
    {
        // add all the state character can do on the season 
        _StateMachine = new StateMachine();

        _atackboss = new AtackBoss(this, _StateMachine);
        _runBoss = new RunBoss(this, _StateMachine);
        _jumpBoss = new JumpBoss(this, _StateMachine);
        _bigJumpAttack = new BigJumpAttack(this, _StateMachine);
    }
    private void Start()
    {
        // add gemcomponent and something like a tool to make condition 
        animator = GetComponent<Animator>();
        CurrentHeal = maxHealth;
        _StateMachine.InititeNewState(_jumpBoss);
        RB= GetComponent<Rigidbody2D>();
        GroundCheck = Physics2D.OverlapCircle(GroundcheckPos.transform.position, radiusGroundCheck, groundLayer);
        GroundLanding = Physics2D.OverlapCircle(GroundcheckPos.position, raiusGroundLanding, groundLayer);
    }
    private void Update()
    {
        // add frame update from state  framUpdate  on update
        Debug.Log(CurrentHeal);
        _StateMachine.CurrentState.FrameUpdate();
        distancePlayerBoss = EnemyPos.transform.position.x - PlayerPos.transform.position.x;
        
        // dodge situation  distanceplayerboss will be - when compare some condition
        if (EnemyPos.transform.position.x < PlayerPos.transform.position.x) 
        {
            distancePlayerBoss = -distancePlayerBoss;
        }
    }
    private void FixedUpdate()
    {
        // add frame fixupdate from state physicupdate on fixupdate
        _StateMachine.CurrentState.PhysicUpdate();
    }
    // if want to run coroutine by state individual use this to call
    public void CallCourotine(IEnumerator CoroutineName) 
    {
        StartCoroutine(CoroutineName);
    }
    public void DelaySomething(string MethodName,float time) 
    {
        Invoke(MethodName,time);
    }
    #region Heal and Damages
    // just heal and damgaes easy
    public void Damage(float damageAmount)
    {
        CurrentHeal = CurrentHeal - damageAmount;
        if (CurrentHeal <= 0)
        {
            die();
        }
    }

    public void die()
    {

    }
    #endregion
    #region Action Boss
    public void EnemyAction(Vector2 velocity)
    {

    }

    public void checkForLeftOrRightFacing(Vector2 velocity)
    {

    }
    #endregion
    #region DistanceCheck
    public void SetAgroStatus(bool isAggroed)
    {
        IsAgroed = isAggroed;
    }

    public void SetStrikingDistanceBool(bool isWithStringkingDistance)
    {
        IsWithStrikingDistance = isWithStringkingDistance;
    }
    #endregion
    #region Animation Trigger
    // make a event when you want call function when character do this animation

    private void AnimationTriggerEvent(AnimationTriggerType triggertype)
    {
        _StateMachine.CurrentState.AnimationTriggerEvent(triggertype);
    }
    // list to what is  animation you want to add ( remember need to add specifically  from state)
    public enum AnimationTriggerType
    {
        Jump,
        JumpLanding,
        JumpAnticipate,
        Idle,
        AttackAnticipate,
        Attack,
        AttackRecover,
        Idle1,
        runAniticipate,
        running,
        JumpFinalAttack1,
        JumpFinalAttack2,
        JumpFinalAttack3,
        Idle2,
        JumpFinalRecover

    }
    public void flip()
    {
        // bacause Con boss mac dinh da lat X 1 lan nen phai lam nguoc dieu kien if < tuc la if > ; if > tuc la if <
        if (isFacingLeft && PlayerPos.position.x < EnemyPos.position.x)
        {
            EnemyPos.transform.Rotate(0, 180, 0);
            isFacingLeft = false;
        }
        else if (!isFacingLeft && PlayerPos.position.x > EnemyPos.position.x)
        {
            EnemyPos.transform.Rotate(0, 180, 0);
            isFacingLeft = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHeal>().EnemyDamages(1, -transform.right);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
    #endregion
    private void OnDrawGizmosSelected()
    {
    }
}
