using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    private Enemy enemy => GetComponentInParent<Enemy>();
   
    
    private void AnimationTrigger()
    {
        enemy.AnimationTrigger();
    }

    protected virtual void Start()
    {
       

    }
    private  void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackChenck.position, enemy.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
              
                PlayerStats target = hit.GetComponent<PlayerStats>();

                enemy.stats.DoDamage(target);

               
               
            }
                
            
        }
    }

   private void SpeicalAttackTrigger()
    {
        enemy.AnimationAttackTrigger();
    }

    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
    

    public void GoToPool()
    {
        enemy.Release();
    }
}
