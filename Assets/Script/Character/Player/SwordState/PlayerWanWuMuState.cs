using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWanWuMuState : PlayerState
{
    private bool skillUsed;
    private float flyTime=.4f;
    private float defaultGravirty;
    public PlayerWanWuMuState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        defaultGravirty = player.rb.gravityScale;
        skillUsed = false;
        stateTimer = flyTime;
        rb.gravityScale = 0;
       
    }

    public override void Exit()
    {
        base.Exit();
        player.rb.gravityScale = defaultGravirty;
        player.currentRenter.color = new Color(1, 1, 1, 255);
    }

    public override void Update()
    {
        base.Update();

        
        if (stateTimer > 0)
            rb.velocity = new Vector2(0, 15);

        if (stateTimer < 0&& player.skill.wanWuMu.wanWuMuDuration>0)
        {
           
            rb.velocity = new Vector2(0, 0);                         

            if (!skillUsed)
            {
                if(player.skill.wanWuMu.CanUseSkill())
                skillUsed = true;
            }
                
        }

        
            



    }
}
