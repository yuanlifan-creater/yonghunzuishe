using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchSwordState : PlayerState
{
    private Transform sword;
    public PlayerCatchSwordState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        sword = player.sword.transform;
        if (player.transform.position.x > sword.position.x && player.facingDir == 1)
            player.Flip();
        else if (player.transform.position.x < sword.position.x && player.facingDir == -1)
            player.Flip();

        rb.velocity = new Vector2(5 * -player.facingDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
     
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
            stateMachine.ChangState(player.idleState);
    }
}
