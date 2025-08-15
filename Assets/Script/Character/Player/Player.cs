using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    [Header("人物不同武器状态")]
    public Sprite[] playerSprite;
    public SpriteRenderer currentRenter;
    [SerializeField] private int currentIndex = 0;
    public bool swordState = true;
    public bool qcState = false;
    public Sprite playerAvatar;

    [Header("攻击信息")]
    public Vector2[] attackMovement;
    public float counterAttackDuration;

    [Header("移动信息")]
    public float moveSpeed;
    public float jumpSpeed;
    [Header("冲刺信息")]
    public float dashSpeed;
    public float dashDuration;
    public bool isInTimeFreeze;
    public float dashDir { get; private set; }
    public GameObject sword;
    public GameObject shengQinang;

    public SkillManager skill { get; private set; }
    public bool isClimbWall;

    private string defaultLayer = "Player";
    private string climbingLayer = "PlayerClimbing";
    public bool isClimbing = false;
    public bool canSlowDownQuickly = true;
    public bool isPlayerInAir;

    public int playerMoney;
    
    #region//状态״̬
    public PlayerStateMachine stateMachine { get; private set; }
    #region//长枪状态
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerPQState pQState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    public PlayerSwordDashState swordDashState { get; private set; }
    public PlayerSwordSlideState swordSlideState { get; private set; }
   public PlayerSwordWallJumpState swordWallJumpState { get; private set; }
    public PlayerSwordPrimyAttackState swordPrimyAttackState { get; private set; }
    public PlayerSwordDashAttackState swordDashAttackState { get; private set; }
    public swordCounterAttackState swordcounterAttackState { get; private set; }
    public PlayerSwordAimState swordAimState { get; private set; }
    public PlayerCatchSwordState catchSwordState { get; private set; }
    public PlayerTimeFreezeState timeFreezeState { get; private set; }
    public PlayerWanWuMuState wanWuMuState { get; private set; }
    public PlayerShunShiZhanState shunShiZhanState { get; private set; }
    #endregion
    #region//剑与盾状态
    public PlayerQCIdleState qcIdleState { get; private set; }
    public PlayerQCMoveState qcMoveState { get; private set; }
    public PlayerQCJumpState qcJumpState { get; private set; }
    public PlayerQCAirState qcAirState { get; private set; }
    public PlayerDieFromSkyState dieFromSkyState { get; private set; }
    


    #endregion

    [SerializeField] private Transform tzCheck;
    [SerializeField] private float tzCheckDistance;
    [SerializeField] private LayerMask whatIsTz;
    
    #endregion
    protected override void Awake()
    {
        base.Awake();

       

        currentRenter = GetComponentInChildren<SpriteRenderer>();
        
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState  = new PlayerAirState(this, stateMachine, "Jump");
        pQState = new PlayerPQState(this, stateMachine, "PQ");
        deadState = new PlayerDeadState(this, stateMachine, "Die");

        swordDashState = new PlayerSwordDashState(this, stateMachine, "SwordDash");
        swordSlideState = new PlayerSwordSlideState(this, stateMachine, "swordWallSlide");
       swordWallJumpState=new PlayerSwordWallJumpState(this, stateMachine, "Jump");
        swordPrimyAttackState = new PlayerSwordPrimyAttackState(this, stateMachine, "SwordAttack");
        swordDashAttackState = new PlayerSwordDashAttackState(this, stateMachine, "swordDashAttack");
        swordcounterAttackState = new swordCounterAttackState(this, stateMachine, "swordCounterAttack");
        swordAimState = new PlayerSwordAimState(this, stateMachine, "aimSword");
        catchSwordState = new PlayerCatchSwordState(this, stateMachine, "catchSword");
        timeFreezeState = new PlayerTimeFreezeState(this, stateMachine, "Idle");
        wanWuMuState=new PlayerWanWuMuState(this, stateMachine, "Jump");
        shunShiZhanState = new PlayerShunShiZhanState(this, stateMachine, "SwordDash");
        


        qcIdleState = new PlayerQCIdleState(this, stateMachine, "DarkState");
        qcMoveState = new PlayerQCMoveState(this, stateMachine, "QCMove");
        qcJumpState = new PlayerQCJumpState(this, stateMachine, "QCJump");
        qcAirState=new PlayerQCAirState(this, stateMachine, "QCJump");
        dieFromSkyState = new PlayerDieFromSkyState(this, stateMachine, "DieFromSky");

    }

   protected override void Start()
    {
        base.Start();
        skill = SkillManager.instance;
        stateMachine.Initialize(idleState);
        

    }

    private void OnEnable()
    {
         EventHandler.BeforeSceneUnLoadEvent+= OnBeforeSceneUnLoadEvent;
        EventHandler.AfterSceneUnLoadEvent += OnAfterSceneUnLoadEvent;
         EventHandler.MoveToPosition += OnMoveToPosition; 
    }

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnLoadEvent -= OnBeforeSceneUnLoadEvent;
        EventHandler.AfterSceneUnLoadEvent -= OnAfterSceneUnLoadEvent;
        EventHandler.MoveToPosition -= OnMoveToPosition;
    }

    private void OnMoveToPosition(Vector3 targetPosition)
    {
        transform.position=targetPosition;
    }

    private void OnAfterSceneUnLoadEvent()
    {
        
    }

    private void OnBeforeSceneUnLoadEvent()
    {
        
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

        if (Input.GetKeyDown(KeyCode.O))
        {
            qcState = !qcState;
            swordState = !swordState;
            SwitchToNextCharacter();
        }
        PlayerUseSkill();


        SlowDownQuick();


        

       
    }

    private void PlayerUseSkill()
    {
        CheckForDashInput();

        if (Input.GetKeyDown(KeyCode.R))
        {

            stateMachine.ChangState(timeFreezeState);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            stateMachine.ChangState(shunShiZhanState);
        };
        

        if (Input.GetKeyDown(KeyCode.F))
            skill.CryStal.CanUseSkill();
    }

    public void AssignNewSword(GameObject newSword)
    {
        sword = newSword;
    }

    public void AssignNewshengChang(GameObject newShengQinang)
    {
        shengQinang = newShengQinang;
    }
    public void CatchTheSword()
    {
        stateMachine.ChangState(catchSwordState);
        Destroy(sword);
    }

    public void DestroyTheShengQiang()
    {
       
        Destroy(shengQinang);
    }
    private void CheckForDashInput()
    {
        ///if (skill.dash.dashUnlocked == false)
            //return;

        if (isInTimeFreeze)
            return;
        
        if (Input.GetKeyDown(KeyCode.L) && SkillManager.instance.dash.CanUseSkill())
        {
            
            dashDir = Input.GetAxisRaw("Horizontal");

            

            if (dashDir == 0)
                dashDir = facingDir;

           
            stateMachine.ChangState(swordDashState);
        }
            
    }
    
    void SwitchToNextCharacter()
    {
        if (playerSprite.Length == 0) return;
        currentIndex = (currentIndex + 1) % playerSprite.Length;     
        currentRenter.sprite = playerSprite[currentIndex];
        Debug.Log("当前角色: " + currentIndex);
    }

    private void PlayerPQ()
    {
        if (Input.GetKey(KeyCode.W))
        {
            stateMachine.ChangState(pQState);
            
        }
           
    }
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyHead>())
        {
           
                rb.velocity = new Vector2(moveSpeed * facingDir , jumpSpeed /3);
        }

        



    }
    public bool isTZDetected() => Physics2D.Raycast(tzCheck.position, Vector2.down, tzCheckDistance, whatIsTz);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(tzCheck.position, new Vector3(tzCheck.position.x, tzCheck.position.y - tzCheckDistance));
    }

    public void PlayerExitState()
    {
        stateMachine.ChangState(airState);
    }

    


    public void StartClimbing()
    {
        isClimbing = true;
        gameObject.layer = LayerMask.NameToLayer(climbingLayer);
    }

    // 离开梯子或到达顶部时调用
    public void StopClimbing()
    {
        isClimbing = false;
        gameObject.layer = LayerMask.NameToLayer(defaultLayer);
    }

    private void SlowDownQuick()
    {
         if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.K)&&canSlowDownQuickly)
        {
            rb.velocity = new Vector2(0, -50);
             StartCoroutine("SlowDownFast");
            
            return;
        }

        if (isTZDetected())
            PlayerPQ();

        if (isGroundDetected() && !isTZDetected())
            StopClimbing();

    }
   


    public IEnumerator SlowDownFast()
    {
        StartClimbing();
        yield return new WaitForSeconds(.2f);
        StopClimbing();
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangState(deadState);
    }

}






