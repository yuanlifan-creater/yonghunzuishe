using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBattleState : EnemyState
{
    private Enemy_Archer enemy;
    private Transform player;
    private int moveDir;

    public ArcherBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Archer enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
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
        base.Update();
        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;

            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanJump())
                    stateMachine.ChangeState(enemy.fgState);

                if (CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 15)
                stateMachine.ChangeState(enemy.idleState);
        }

       
       if (player.position.x > enemy.transform.position.x)
           moveDir = 1;
       else if (player.position.x < enemy.transform.position.x)
           moveDir = -1;
     
      

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

    private bool CanJump()
    {
        if (Time.time >= enemy.lastTimeJumped + enemy.jumpCooldown)
        {

            enemy.lastTimeJumped = Time.time;
            return true;
        }

        return false;
    }

}
