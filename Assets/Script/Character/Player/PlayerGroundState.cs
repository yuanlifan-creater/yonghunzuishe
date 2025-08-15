using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();


        if (Input.GetKeyDown(KeyCode.G))
        {
            stateMachine.ChangState(player.wanWuMuState);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && !player.sword &&player.skill.sword.swordUnLocked)
            stateMachine.ChangState(player.swordAimState);
        else if (Input.GetKeyDown(KeyCode.Mouse1) && player.sword)
            player.sword.GetComponent<SwordSkillController>().ReturnSword();

        if (Input.GetKeyDown(KeyCode.J))
            stateMachine.ChangState(player.swordPrimyAttackState);

        if (!player.isGroundDetected())
            stateMachine.ChangState(player.airState);

        if (Input.GetKeyDown(KeyCode.Q))
            stateMachine.ChangState(player.swordcounterAttackState);

        

        if (Input.GetKeyDown(KeyCode.K) && player.isGroundDetected() && player.swordState)
            stateMachine.ChangState(player.jumpState);
        else if (Input.GetKeyDown(KeyCode.K) && player.isGroundDetected() && player.qcState)
            stateMachine.ChangState(player.qcJumpState);

        if (Input.GetKeyDown(KeyCode.Z))
            stateMachine.ChangState(player.dieFromSkyState);
    }

    // Start is called before the first frame update
   
}
