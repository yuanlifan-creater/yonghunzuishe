using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_JinJun : Enemy
{
    public EnemyHead enemyHead;

    public JinJunIdleState idleState;
    public JinJunMoveState moveState;
    public JinJunAttackState attackState;
    public JinJunBattleState battleState;
    public JinJunDeadState deadState;



    protected override void Awake()
    {
        base.Awake(); 
        idleState = new JinJunIdleState(this, stateMachine, "Idle", this);
        moveState= new JinJunMoveState(this, stateMachine, "run", this);
        attackState= new JinJunAttackState(this, stateMachine, "Attack", this);
        battleState = new JinJunBattleState(this, stateMachine, "Move", this);
        deadState=new JinJunDeadState(this, stateMachine, "Death", this);
        SetUpDefaultFacingDIR(-1);
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

    public void playerCaiTou()
    {
        if (enemyHead.isPlayerCaiTou)
        {
           
        }
    }

    
    // Start is called before the first frame update

}
