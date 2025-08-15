using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YyjslIdleState : EnemyState
{
    private Enemy_YYJSL enemy;
    private Transform player;
    public YyjslIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_YYJSL _enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();


        if (Vector2.Distance(player.transform.position, enemy.transform.position) < 7)
            enemy.BossFightBegan = true;






        if(Input.GetKeyDown(KeyCode.H))
            stateMachine.ChangeState(enemy.telePortState);

        if (stateTimer < 0 && enemy.BossFightBegan)
            stateMachine.ChangeState(enemy.battleState);

    }
}
