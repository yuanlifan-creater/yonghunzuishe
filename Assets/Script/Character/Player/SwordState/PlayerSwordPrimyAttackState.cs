using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordPrimyAttackState : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float combooWidow = 2;
    public PlayerSwordPrimyAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        xInput = 0;
        if (comboCounter > 2 || Time.time>=lastTimeAttacked+combooWidow)
            comboCounter = 0;
        player.anim.SetInteger("comboCounter", comboCounter);
        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        lastTimeAttacked = Time.time;
        
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            player.ZeroVelocity();


        if (triggerCalled)
            stateMachine.ChangState(player.idleState);
    }
}
