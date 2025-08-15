using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQCAirState : PlayerState
{
    public PlayerQCAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        if (player.isGroundDetected() && player.qcState)
            stateMachine.ChangState(player.qcIdleState);
    }
}
