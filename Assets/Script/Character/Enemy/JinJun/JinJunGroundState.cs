using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JinJunGroundState : EnemyState
{
    protected Enemy_JinJun enemy;
    protected Transform player;
    public JinJunGroundState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_JinJun _enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = _enemy;
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
        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.transform.position) < 2)
            stateMachine.ChangeState(enemy.battleState);

        enemy.playerCaiTou();
    }
}
