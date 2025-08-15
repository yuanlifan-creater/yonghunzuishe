using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleGroundState : EnemyState
{
    protected Enemy_Skele enemy;
    protected Transform player;
    
    public SkeleGroundState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skele enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.IsPlayerDetected()||Vector2.Distance(enemy.transform.position,player.transform.position)<2)
            stateMachine.ChangeState(enemy.battleState);

        enemy.playerCaiTou();
    }
}
