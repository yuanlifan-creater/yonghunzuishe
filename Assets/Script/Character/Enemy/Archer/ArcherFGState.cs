using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherFGState : EnemyState
{
    private Enemy_Archer enemy;
    public ArcherFGState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Archer _enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(enemy.jumpVelocity.x * -enemy.facingDir, enemy.jumpVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

       

        if (rb.velocity.y < 0 && enemy.isGroundDetected())
            stateMachine.ChangeState(enemy.idleState);

    }
}
