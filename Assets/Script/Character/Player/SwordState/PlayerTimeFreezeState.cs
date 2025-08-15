using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeFreezeState : PlayerState
{
    private float freezeTime =.4f;
    private bool skillUsed;
    private float defaultGravity;
    // Start is called before the first frame update
    public PlayerTimeFreezeState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        defaultGravity = player.rb.gravityScale;
        stateTimer = freezeTime;
        skillUsed = false;
        rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
        player.rb.gravityScale = defaultGravity;

        player.isInTimeFreeze = false;

    }

    public override void Update()
    {
        base.Update();

        player.isInTimeFreeze = true;

       
        if (stateTimer > 0)
        {
            player.ZeroVelocity();
            
        }

        if (stateTimer < 0)
        {
           

            if (!skillUsed)
            {
                if(player.skill.timeFreeze.CanUseSkill())
                skillUsed = true;

            }
               
        }

        if (player.skill.timeFreeze.SkillCompleted())
            stateMachine.ChangState(player.idleState);

    }
}
