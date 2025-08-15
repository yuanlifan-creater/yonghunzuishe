using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YyjslSpellCastState : EnemyState
{
    private Enemy_YYJSL enemy;
    private int amountOfSpells;
    private float spellTimer;
    public YyjslSpellCastState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_YYJSL _enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        amountOfSpells = enemy.amountOfSpells;
        spellTimer =  .5f;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeCast=  Time.time;
    }

    public override void Update()
    {
        base.Update();
        spellTimer -= Time.deltaTime;


        if (CanCast())
        {
            enemy.CastSpell();
        }
      
        if(amountOfSpells<=0)
            stateMachine.ChangeState(enemy.telePortState);
    }


    private bool CanCast()
    {
        if (amountOfSpells > 0 && spellTimer <0)
        {
            amountOfSpells--;
            spellTimer = enemy.spellCoolDown;
            return true;
        }
           

        return false;
    }

}
