using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordSlideState : PlayerState
{
    public PlayerSwordSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
       
        player.facingDir = player.facingDir * -1;


       

    }

    public override void Exit()
    {
        base.Exit();
       
        player.facingDir = player.facingDir * -1;

        
        


    }

    public override void Update()
    {
        base.Update();
       
        if (Input.GetKeyDown(KeyCode.K))
        {

            stateMachine.ChangState(player.swordWallJumpState);
            return;
           
        }


        switch (yInput)
        {
            case > 0:
                rb.velocity = new Vector2(0, rb.velocity.y * .5f);
                break;
            case 0:
                rb.velocity = new Vector2(0, rb.velocity.y);
                break;
            case < 0:
                rb.velocity = new Vector2(0, rb.velocity.y * 1.2f);
                break;
        }

        if ((xInput != 0&&xInput==player.facingDir) || player.isGroundDetected())
        {
           
            stateMachine.ChangState(player.idleState);
            player.Flip();

        }
        



    }
}
