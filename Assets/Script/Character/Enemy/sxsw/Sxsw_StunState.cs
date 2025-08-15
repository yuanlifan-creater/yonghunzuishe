using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sxsw_StunState : EnemyState
{
    public bool isStun;
    private Enemy_Sxsw enemy;
    private int playerLayer;
    private int enemyLayer;

    public Sxsw_StunState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Sxsw _enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        playerLayer = LayerMask.NameToLayer("Player");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        stateTimer = 3f;
        isStun = true;
        
    }

    public override void Exit()
    {
        base.Exit();
        enemy.YunXuan.SetActive(false);
        isStun = false;
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
    }

    public override void Update()
    {
        base.Update();

        enemy.YunXuan.SetActive(true);
        if (stateTimer > 0)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);
            enemy.ZeroVelocity();
            
        }
        else if(stateTimer<=0)
        {
            
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
