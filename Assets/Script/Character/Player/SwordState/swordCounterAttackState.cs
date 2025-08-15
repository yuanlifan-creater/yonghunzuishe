using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordCounterAttackState : PlayerState
{
    public swordCounterAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("successswordCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.ZeroVelocity();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackChenck.position, player.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>().CanbeStunned())
            {
                stateTimer = 2;
                player.anim.SetBool("successswordCounterAttack", true);
            }
                
        }

        if (stateTimer < 0 || triggerCalled)
            stateMachine.ChangState(player.idleState);
    }
}
