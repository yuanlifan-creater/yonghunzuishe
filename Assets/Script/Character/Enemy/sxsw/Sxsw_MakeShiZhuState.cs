using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sxsw_MakeShiZhuState : EnemyState
{
    private Enemy_Sxsw enemy;
   
    public Sxsw_MakeShiZhuState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Sxsw _enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = _enemy;
    }

   

    public override void Enter()
    {
        base.Enter();

        enemy.shiZhuAmount = 8;
        enemy.UseMakeShiSkill();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.canMakeShizhu = false;
    }

    public override void Update()
    {
        base.Update();
        enemy.ZeroVelocity();

        if (enemy.shiZhuAmount ==0)
            stateMachine.ChangeState(enemy.battleState);
        
    }
}
