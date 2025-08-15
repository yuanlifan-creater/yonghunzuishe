using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQCIdleState : PlayerGroundState
{
    public PlayerQCIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        if (player.swordState)
            stateMachine.ChangState(player.idleState);

        if (xInput != 0 && player.qcState)
        {
            stateMachine.ChangState(player.qcMoveState);
        }
    }
}
