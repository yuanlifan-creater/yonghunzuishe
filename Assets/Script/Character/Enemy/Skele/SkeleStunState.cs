using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleStunState : EnemyState
{
    private Enemy_Skele enemy;
    public SkeleStunState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName,Enemy_Skele enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.fx.InvokeRepeating("RedBlink", 0, .1f);
        stateTimer = enemy.stunDuration;
        rb.velocity=new Vector2(-enemy.facingDir * enemy.stunDirection.x, enemy.stunDirection.y);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.fx.Invoke("CancelRedBlink", 0);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.battleState);
    }
}
