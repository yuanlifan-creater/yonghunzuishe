using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isPlayerInAir = true;
       

    }

    public override void Exit()
    {
        base.Exit();
        player.isPlayerInAir =false;
    }

    public override void Update()
    {
        base.Update();
       

        if (player.isGroundDetected() && player.swordState)
            stateMachine.ChangState(player.idleState);

        if (player.isWallDetected() && player.swordState)
            stateMachine.ChangState(player.swordSlideState);

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.K))
        {
            rb.velocity = new Vector2(0, -50);
            
            return;
        }
        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
       

    }

    // Start is called before the first frame update

}
