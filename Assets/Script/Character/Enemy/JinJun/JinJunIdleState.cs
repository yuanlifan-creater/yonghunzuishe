using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JinJunIdleState : JinJunGroundState
{
    public JinJunIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_JinJun _enemy) : base(enemyBase, stateMachine, animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.moveState);




    }
}
