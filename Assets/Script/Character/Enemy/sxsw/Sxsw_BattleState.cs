using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sxsw_BattleState : EnemyState
{
    private Enemy_Sxsw enemy;
    private Transform player;
    private int moveDir;
    private float yVelocity;
    private bool isDHstart=false;
    private float a=18f;
    public Sxsw_BattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Sxsw _enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        yVelocity = enemy.rb.velocity.y;
        player = PlayerManager.instance.player.transform;
      //  if (player.GetComponent<CharacterStats>().isDead)
      //      stateMachine.ChangeState(enemy.idleState);

       // enemy.StartCoroutine("Startattle");
    }
    public override void Update()
    {

        
       

        base.Update();

        if (Input.GetKeyDown(KeyCode.U))
        {

            enemy.PrepareSkill();
           

        }
        enemy.UpdateJump();


        if (enemy.canMakeShizhu)
        {
            stateMachine.ChangeState(enemy.makeShiZhuState);
        }
           
        

        //enemy.IsPlayerDetected().distance

        

        if (enemy.isJumping) return;

        if (Input.GetKeyDown(KeyCode.Y))
        {
            stateMachine.ChangeState(enemy.dashState);
           
        }
        if (enemy.dashState.isDash||enemy.stunState.isStun)
            return;

        if (Vector2.Distance(player.transform.position, enemy.transform.position) < enemy.attackDistance)
        {

            if (CanAttack())
            {

                stateMachine.ChangeState(enemy.attackState);

            }

        }

        if (player.position.x == enemy.transform.position.x)
            return;

        if (player.position.x > enemy.transform.position.x)
          moveDir = 1;
      else if (player.position.x < enemy.transform.position.x)
          moveDir = -1;
        

        if (!isDHstart)
      {
          a -= Time.deltaTime;
          enemy.SetVelocity(0, 0);
          if (a <= 0)
              isDHstart = true;
      }
      else
      {
            if (Vector2.Distance(player.position, enemy.transform.position) > enemy.attackDistance)
                enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
            else
                enemy.ZeroVelocity();

        }
    



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

