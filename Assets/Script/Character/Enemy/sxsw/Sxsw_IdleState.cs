using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sxsw_IdleState : Sxsw_GroundState
{
    public Sxsw_IdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Sxsw _enemy) : base(enemyBase, stateMachine, animBoolName, _enemy)
    {
    }

    // Start is called before the first frame update
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
            stateMachine.ChangeState(enemy.battleState);

    }
}
