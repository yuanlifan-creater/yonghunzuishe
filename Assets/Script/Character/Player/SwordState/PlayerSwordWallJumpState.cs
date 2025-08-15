using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordWallJumpState : PlayerState
{
    public PlayerSwordWallJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1f;
        player.SetVelocity(player.moveSpeed* -player.facingDir, player.jumpSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangState(player.airState);
    }
}
