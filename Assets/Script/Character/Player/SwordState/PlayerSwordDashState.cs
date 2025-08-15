using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordDashState : PlayerState
{
    public PlayerSwordDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skill.dash.cloneDashStart();
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y); 
        
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.dashSpeed * player.dashDir, 0);
        if (Input.GetKeyDown(KeyCode.J))
        {
            stateMachine.ChangState(player.swordDashAttackState);
            return;
        }

       
           


        if (stateTimer < 0)
            stateMachine.ChangState(player.idleState);

       

    }
}
