using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sxsw : Enemy
{

   public Sxsw_AttackState attackState{ get; private set; }
   public Sxsw_BattleState battleState{ get; private set; }
   public Sxsw_DeadState deadState  { get; private set; }
   public Sxsw_MakeShiZhuState makeShiZhuState { get; private set; }
   public Sxsw_DashState dashState { get; private set; }
   public Sxsw_StunState stunState { get; private set; }
    [Header("技能信息")]
    [SerializeField] private GameObject shiZhu;
    public int shiZhuAmount;
    public GameObject YunXuan;

    public bool canJump;
    private int shiZhuInt=-1;
   private int  removeX;
    private CapsuleCollider2D cp;
    private Quaternion randomRotation;
    #region//跳跃指定地点
    public bool isJumping = false; // 跳跃状态标志
    private Vector2 jumpTarget;    // 跳跃目标位置
    private float jumpDuration;    // 跳跃总时长
    private float jumpTimer;        // 跳跃计时器
    public bool canMakeShizhu;
    
    #endregion
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
       
        //idleState = new Sxsw_IdleState(this, stateMachine, "Idle", this);
       
        battleState = new Sxsw_BattleState(this, stateMachine, "Idle", this);
        attackState = new Sxsw_AttackState(this, stateMachine, "attack", this);
        stunState = new Sxsw_StunState(this, stateMachine, "stun", this);
        dashState = new Sxsw_DashState(this, stateMachine, "dash", this);
        makeShiZhuState = new Sxsw_MakeShiZhuState(this, stateMachine, "attack", this);
        deadState = new Sxsw_DeadState(this, stateMachine, "die", this);
        cp = GetComponent<CapsuleCollider2D>();
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(battleState);
        SetUpDefaultFacingDIR(-1);
    }

    protected override void Update()
    {
        base.Update();

        //   if (Input.GetKeyDown(KeyCode.U))
        //    {
        //        shiZhuAmount = 8;
        //        PrepareSkill();
        //        //stateMachine.ChangeState(makeShiZhuState);
        //    }


    }

    

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }


  
  
  
  
  
  
    

   

    public void MakeShiZhuSkill()
    {
        StartCoroutine("MakeShiZhu");
    }

    private IEnumerator MakeShiZhu()
    {
        
        while (shiZhuAmount>0)
        {
            CreatShiZhuTransform();
            yield return new WaitForSeconds(jumpDuration);
            shiZhuAmount--;
            

            
            
        }
        

    }

    private void CreatShiZhuTransform()
    {
        shiZhuInt = shiZhuInt *-1;

       
        if (shiZhuInt == 1)
        {
            removeX = -6;
            randomRotation = Quaternion.Euler(0f, 0f, 0f);


        }
        else if(shiZhuInt == -1)
        {
            removeX = 7;
            randomRotation = Quaternion.Euler(0f, 180f, 0f);
           
        }

        Vector3 shiZhuTransform = new Vector3(transform.position.x + removeX, transform.position.y - 1.7f, 0);
        GameObject sz = Instantiate(shiZhu, shiZhuTransform, randomRotation);
    }


    public void JumpToCenter(Vector2 targetPosition)
    {
        isJumping = true;
        jumpTarget = targetPosition;
        jumpTimer = 0f;

        // 计算跳跃参数
        Vector2 startPosition = transform.position;
        float gravity = Physics2D.gravity.y * rb.gravityScale;

        // 计算水平距离和所需时间
        float horizontalDistance = Mathf.Abs(targetPosition.x - startPosition.x);
        jumpDuration = Mathf.Clamp(horizontalDistance / moveSpeed, 0.5f, 2f);

        // 设置初始速度
        float deltaX = targetPosition.x - startPosition.x;
        float deltaY = targetPosition.y - startPosition.y;

        float horizontalSpeed = deltaX / jumpDuration;
        float verticalSpeed = (deltaY - 0.5f * gravity * Mathf.Pow(jumpDuration, 2)) / jumpDuration;

        SetVelocity(horizontalSpeed, verticalSpeed);
        
    }

    public void UpdateJump()
    {
       

        if (!isJumping) return;

        jumpTimer += Time.deltaTime;
       
        // 跳跃完成检测（时间到或接近目标）
        if ( Vector2.Distance(transform.position, jumpTarget) < .25f)
        {
            canMakeShizhu = true;
            isJumping = false;

           


        }
    }
        public void PrepareSkill()
    {
        // 停止其他移动逻辑
        //   StopAllCoroutines();
        //   ZeroVelocity();

        // 跳跃到场景中心 
        if (!isJumping)
            JumpToCenter(new Vector2(-2f, 0f));

        // 在跳跃后使用技能（示例：0.8秒后）
        
    }

   public void UseMakeShiSkill()
    {
        StartCoroutine("MakeShiZhu");
        
    }
   
}
