using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleAttackState : EnemyState
{
    private Enemy_Skele enemy;
    public SkeleAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName,Enemy_Skele enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
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
