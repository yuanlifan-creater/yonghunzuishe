using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordDashAttackState : PlayerState
{
    public PlayerSwordDashAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
       
        player.SetVelocity(rb.velocity.x, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {
            if (player.isGroundDetected())
                stateMachine.ChangState(player.idleState);
            else
                stateMachine.ChangState(player.airState);
        }
    }
}
