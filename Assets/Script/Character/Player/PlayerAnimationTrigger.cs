using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackChenck.position, player.attackCheckRadius);
         foreach(var hit in colliders)
        {
            if (hit.GetComponent<ArrowControler>() != null)
                hit.GetComponent<ArrowControler>().FlipArrow();

            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats target = hit.GetComponent<EnemyStats>();

                player.stats.DoDamage(target);

            }
               
           
        }
    }

    private void ThrowSword()
    {
        player.skill.sword.CreateSword();
    }

}
