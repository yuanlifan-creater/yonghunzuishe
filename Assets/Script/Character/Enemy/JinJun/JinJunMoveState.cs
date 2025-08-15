using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JinJunMoveState : JinJunGroundState
{
    public JinJunMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_JinJun _enemy) : base(enemyBase, stateMachine, animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, enemy.rb.velocity.y);

        if (enemy.isWallDetected() || !enemy.isGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }

    }
}
