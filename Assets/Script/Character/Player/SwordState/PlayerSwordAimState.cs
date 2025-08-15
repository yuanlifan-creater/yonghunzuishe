using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAimState : PlayerState
{
    public PlayerSwordAimState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skill.sword.DotsActive(true);
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.ZeroVelocity();
        if (Input.GetKeyUp(KeyCode.Mouse1))
            stateMachine.ChangState(player.idleState);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (player.transform.position.x > mousePosition.x&&player.facingDir == 1)
            player.Flip();
        else if(player.transform.position.x < mousePosition.x&&player.facingDir == -1)
            player.Flip(); 
    }
}
