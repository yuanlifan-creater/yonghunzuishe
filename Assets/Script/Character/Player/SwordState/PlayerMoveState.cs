using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{ 
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        if (xInput == 0)
            stateMachine.ChangState(player.idleState);
    }

    // Start is called before the first frame update






}
