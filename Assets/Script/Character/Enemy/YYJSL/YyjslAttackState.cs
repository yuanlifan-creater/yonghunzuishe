using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YyjslAttackState : EnemyState
{
    private Enemy_YYJSL enemy;

    public YyjslAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_YYJSL _enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.chanceToTeleport += 5;
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
        {
            if (enemy.CanTeleport())
                stateMachine.ChangeState(enemy.telePortState);
            else
                stateMachine.ChangeState(enemy.battleState);
        }
            
    }
}
