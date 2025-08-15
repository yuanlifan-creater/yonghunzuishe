using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherDeadState : EnemyState
{
    private Enemy_Archer enemy;

    public ArcherDeadState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Archer _enemy) : base(enemyBase, stateMachine, animBoolName)
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
