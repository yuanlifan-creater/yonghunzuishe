using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skele_IdleState : SkeleGroundState
{
    public Skele_IdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skele enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
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
