using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieFromSkyState : PlayerState
{
    private bool skillUsed;
   
    public PlayerDieFromSkyState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 3;
        skillUsed = false;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer>0)
        {
            player.ZeroVelocity();
            if (!skillUsed)
            {
                if (player.skill.dieFromSkill.CanUseSkill())
                {
                    skillUsed = true;

                }
            }
        }
           
        else if(stateTimer<0)
        {
            stateMachine.ChangState(player.idleState);
           
        }

        

    }
}
