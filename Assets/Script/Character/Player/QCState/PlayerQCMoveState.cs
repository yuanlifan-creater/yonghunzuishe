using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQCMoveState : PlayerGroundState
{
    public PlayerQCMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        if (xInput == 0 && player.qcState)
            stateMachine.ChangState(player.qcIdleState);
    }
}
