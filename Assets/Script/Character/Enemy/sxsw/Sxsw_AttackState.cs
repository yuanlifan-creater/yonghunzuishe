using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sxsw_AttackState : EnemyState
{
    private Enemy_Sxsw enemy;

    public Sxsw_AttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Sxsw _enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        
        enemy.ZeroVelocity();
        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);
    }
}
