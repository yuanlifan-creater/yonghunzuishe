using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JinJunAttackState : EnemyState
{
    public Enemy_JinJun enemy;
    public JinJunAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_JinJun _enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        enemy = _enemy;
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
