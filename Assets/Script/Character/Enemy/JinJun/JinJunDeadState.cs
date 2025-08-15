using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JinJunDeadState : EnemyState
{
    private Enemy_JinJun enemy;
    public JinJunDeadState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_JinJun _enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.anim.SetBool(enemy.lastAnimBoolName, true);

    }

    public override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(0, 0);
    }
}
