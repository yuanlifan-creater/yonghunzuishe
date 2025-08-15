using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sxsw_DashState : EnemyState
{
    private Enemy_Sxsw enemy;
    private bool canAttack;
    private bool SecondDash;

    public bool isDash;
    public Sxsw_DashState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Sxsw _enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 6f;
        SecondDash = false;
       isDash = true;
    }

    public override void Exit()
    {
        base.Exit();
        isDash = false;
       
    }

    public override void Update()
    {
        base.Update();


        if (enemy.isWallDetected())
        {
            stateMachine.ChangeState(enemy.stunState);
            return;
        }
           

        if (stateTimer >= 0)
        {
            if (stateTimer > 3)
            {
                enemy.SetVelocity(enemy.moveSpeed * 2 * enemy.facingDir, 0);
            }            
               else if (stateTimer <= 3 )
            {

                if (SecondDash == false)
                    enemy.Flip();

                SecondDash = true;
                enemy.SetVelocity(enemy.moveSpeed * 2 * enemy.facingDir, 0);
            }
               
        }
        else
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }

   
}
