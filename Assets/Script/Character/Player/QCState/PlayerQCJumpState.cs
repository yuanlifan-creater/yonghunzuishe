using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQCJumpState : PlayerState
{
    public PlayerQCJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(rb.velocity.x, player.jumpSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (rb.velocity.y < 0 && player.qcState)
            stateMachine.ChangState(player.qcAirState);
    }
}
