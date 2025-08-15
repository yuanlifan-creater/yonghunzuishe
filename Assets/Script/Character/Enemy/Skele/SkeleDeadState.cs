using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkeleDeadState : EnemyState    
{
    private Enemy_Skele enemy;
    public SkeleDeadState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName,Enemy_Skele _enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        enemy = _enemy;
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
