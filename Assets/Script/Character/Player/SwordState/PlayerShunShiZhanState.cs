using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShunShiZhanState : PlayerState
{
    private float prePareTime = .2f;
    private bool skillUsed;
    private float endTime = .4f;
    private int playerLayer;
    private int enemyLayer;
    public PlayerShunShiZhanState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        skillUsed = false;
        stateTimer = prePareTime;
        playerLayer = LayerMask.NameToLayer("Player");
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    public override void Exit()
    {
        base.Exit();
        endTime = .4f;
    }

    public override void Update()
    {
        base.Update();
        endTime -= Time.deltaTime;
        if (stateTimer > 0)
        {
            player.SetVelocity(player.facingDir * player.skill.shunShiZhan.ShunShiZhanSpeed, 0);
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);
        }
           

        if (stateTimer < 0)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
            if (endTime >= .2f)
            {
                rb.velocity = new Vector2(0, 0);
            }else if (endTime < .2f)
            {
                stateMachine.ChangState(player.idleState);
            }
               

            
           
           

            if (!skillUsed)
            {
                if (player.skill.shunShiZhan.CanUseSkill())
                {
                    skillUsed = true;
                   
                }
            }



        }
    }
}
