using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SxswAnimationTrigger : EnemyAnimationTrigger
{
    private Enemy enemy => GetComponentInParent<Enemy>();
    private Enemy_Sxsw sxsw => GetComponentInParent<Enemy_Sxsw>();
    private bool canAttack=true;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    private void SxswAttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackChenck.position, enemy.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null&& canAttack)
            {

                PlayerStats target = hit.GetComponent<PlayerStats>();
                
               enemy.stats.DoDamage(target);
                StartCoroutine(AttackCooldown());
                StartCoroutine("InvincibilityCoroutine");

            }


        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(2f);
        canAttack = true;
    }
}
