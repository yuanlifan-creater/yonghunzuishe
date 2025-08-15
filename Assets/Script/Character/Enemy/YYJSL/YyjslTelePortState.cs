using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YyjslTelePortState : EnemyState
{
    private Enemy_YYJSL enemy;
    public YyjslTelePortState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_YYJSL _enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {
            if (enemy.CanDoSpellCast())
            {
                stateMachine.ChangeState(enemy.castState);
            }
            else
            stateMachine.ChangeState(enemy.battleState);
        }
           
    }




}
