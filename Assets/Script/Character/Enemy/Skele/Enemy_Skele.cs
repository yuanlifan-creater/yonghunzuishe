using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_Skele : Enemy
{

    public EnemyHead enemyHead;
   
    

    #region//״̬状态
    public Skele_IdleState idleState { get; private set; }
    public Skele_MoveState moveState { get; private set; }
    public SkeleBattleState battleState { get; private set; }
    public SkeleAttackState attackState { get; private set; }
    public SkeleDeadState deadState { get; private set; }
    public SkeleStunState stunState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
       
        idleState = new Skele_IdleState(this, stateMachine, "idle",this);
        moveState = new Skele_MoveState(this, stateMachine, "move", this);
        battleState = new SkeleBattleState(this, stateMachine, "move", this);
        attackState = new SkeleAttackState(this, stateMachine, "attack", this);
        stunState=new SkeleStunState(this, stateMachine, "stun", this);
       
        deadState = new SkeleDeadState(this, stateMachine, "die", this);
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

    
    public virtual bool CanbeStunned()
    {
        if (base.CanbeStunned())
        {
            stateMachine.ChangeState(stunState);
            return true;
        }

        return false;
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

  

    

    
}
