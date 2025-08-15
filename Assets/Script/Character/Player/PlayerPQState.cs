using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPQState : PlayerState
{
    private float defaultGravity;
    public PlayerPQState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(0, rb.velocity.y);
        defaultGravity = rb.gravityScale;
        rb.gravityScale = 0;
        player.isClimbWall = true;
        player.StartClimbing();
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = defaultGravity;
        player.isClimbWall = false;
        //player.StopClimbing();
    }

    public override void Update()
    {
        base.Update();
        if (yInput != 0)
        {
            player.anim.SetBool("PQStop", false);

            if (Input.GetKey(KeyCode.W))
                rb.velocity = new Vector2(rb.velocity.x, 5);

            if (Input.GetKey(KeyCode.S))
                rb.velocity = new Vector2(rb.velocity.x, -5);

            if ((player.isGroundDetected()&& Input.GetKey(KeyCode.S))||!player.isClimbing)
                stateMachine.ChangState(player.idleState);
        }
        else if (yInput == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            player.anim.SetBool("PQStop", true);
        }

        if (Input.GetKeyDown(KeyCode.K))
            stateMachine.ChangState(player.jumpState);

        if(xInput!=0)
            stateMachine.ChangState(player.airState);



    }
}
