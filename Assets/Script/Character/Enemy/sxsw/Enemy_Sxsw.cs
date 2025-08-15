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
    [Header("������Ϣ")]
    [SerializeField] private GameObject shiZhu;
    public int shiZhuAmount;
    public GameObject YunXuan;

    public bool canJump;
    private int shiZhuInt=-1;
   private int  removeX;
    private CapsuleCollider2D cp;
    private Quaternion randomRotation;
    #region//��Ծָ���ص�
    public bool isJumping = false; // ��Ծ״̬��־
    private Vector2 jumpTarget;    // ��ԾĿ��λ��
    private float jumpDuration;    // ��Ծ��ʱ��
    private float jumpTimer;        // ��Ծ��ʱ��
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

        // ������Ծ����
        Vector2 startPosition = transform.position;
        float gravity = Physics2D.gravity.y * rb.gravityScale;

        // ����ˮƽ���������ʱ��
        float horizontalDistance = Mathf.Abs(targetPosition.x - startPosition.x);
        jumpDuration = Mathf.Clamp(horizontalDistance / moveSpeed, 0.5f, 2f);

        // ���ó�ʼ�ٶ�
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
       
        // ��Ծ��ɼ�⣨ʱ�䵽��ӽ�Ŀ�꣩
        if ( Vector2.Distance(transform.position, jumpTarget) < .25f)
        {
            canMakeShizhu = true;
            isJumping = false;

           


        }
    }
        public void PrepareSkill()
    {
        // ֹͣ�����ƶ��߼�
        //   StopAllCoroutines();
        //   ZeroVelocity();

        // ��Ծ���������� 
        if (!isJumping)
            JumpToCenter(new Vector2(-2f, 0f));

        // ����Ծ��ʹ�ü��ܣ�ʾ����0.8���
        
    }

   public void UseMakeShiSkill()
    {
        StartCoroutine("MakeShiZhu");
        
    }
   
}
