using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Archer : Enemy
{
   [Header("箭矢信息")]
    [SerializeField] private GameObject arrow;
    [SerializeField] private float arrowSpeed;
    [SerializeField] private float flyTime;

    public Vector2 jumpVelocity;
    public float jumpCooldown;
    public float safeDistance;
    [HideInInspector] public float lastTimeJumped;
    #region
    public EnemyHead enemyHead;
    // Start is called before the first frame update
    public ArcherIdleState idleState { get; private set; }
    public ArcherMoveState moveState { get; private set; }
    public ArcherBattleState battleState { get; private set; }
    public ArcherAttackState attackState { get; private set; }
    public ArcherDeadState deadState { get; private set; }

    public ArcherFGState fgState { get; private set; }
    #endregion

    protected override void Awake()  
    {
        base.Awake();
        idleState = new ArcherIdleState(this, stateMachine, "idle", this);
        moveState = new ArcherMoveState(this, stateMachine, "move", this);
        battleState = new ArcherBattleState(this, stateMachine, "idle", this);
        attackState = new ArcherAttackState(this, stateMachine, "attack", this);
        fgState = new ArcherFGState(this, stateMachine, "Jump", this);

        deadState = new ArcherDeadState(this, stateMachine, "die", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);

    }

    protected override void Update()
    {
        base.Update();
        playerCaiTou();


    }

   

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }


    public void playerCaiTou()
    {
        if (enemyHead.isPlayerCaiTou)
        {
            
        }
    }

   



    public void Release()
    {
        //_pool.Release(gameObject); // 回收自身到对象池

    }

    public override void AnimationAttackTrigger()
    {
        GameObject newArrow = Instantiate(arrow,attackChenck.position, Quaternion.identity);
        newArrow.GetComponent<ArrowControler>().SetUpArrow(arrowSpeed*facingDir,stats,flyTime);
    }

}
