using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YyjslBattleState : EnemyState
{
    private Enemy_YYJSL enemy;
    private Transform player;
    private int moveDir;

    public YyjslBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_YYJSL _enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
        if (player.GetComponent<CharacterStats>().isDead)
            stateMachine.ChangeState(enemy.idleState);

        //enemy.StartCoroutine("Startattle");
    }
    public override void Update()
    {

        if (enemy.IsPlayerDetected())
        {
            //stateTimer = enemy.battleTime;

            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
                else
                    stateMachine.ChangeState(enemy.idleState);
            }
        }
        

        base.Update();
        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            moveDir = -1;

        if (enemy.IsPlayerDetected() && enemy.IsPlayerDetected().distance < enemy.attackDistance - .1f)
            return;


        enemy.SetVelocity(enemy.moveSpeed * moveDir, enemy.rb.velocity.y);

    }
    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }

}
