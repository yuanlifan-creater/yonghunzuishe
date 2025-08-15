using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.ZeroVelocity();

        
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void Update()
    {
        base.Update();
      
        if (player.qcState&&xInput==0)
            stateMachine.ChangState(player.qcIdleState);

        if (xInput != 0 && player.swordState)
            stateMachine.ChangState(player.moveState);

        

    }

    // Start is called before the first frame update
    
    
}
