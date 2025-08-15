using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryStalSkillController : MonoBehaviour
{
    private float cryStalExistTimer;
    private bool canMove;
    private bool canExplode;
    private float moveSpeed;
    private bool canGrow;
    private float maxSize;
    private float growSpeed;
    private Transform closestTarget;
    private Player player;
    private CircleCollider2D cd => GetComponent<CircleCollider2D>();
    private Animator anim => GetComponent<Animator>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetupCrystal(float _cryStalExistTimer,bool _canExplode,bool _canMove,float _moveSpeed, float _maxSize, float _growSpeed, bool _canGrow,Transform _closestTransform,Player _player)
    {
        cryStalExistTimer = _cryStalExistTimer;
        canExplode = _canExplode;
        canMove = _canMove;
        moveSpeed = _moveSpeed;
        maxSize = _maxSize;
        growSpeed = _growSpeed;
        canGrow = _canGrow;
        closestTarget = _closestTransform;
        player = _player;

    }

    // Update is called once per frame
    void Update()
    {
        cryStalExistTimer -= Time.deltaTime;
        if (cryStalExistTimer <= 0)
        {
            FinishCrystal();
        }
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, closestTarget.position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, closestTarget.position) < 1)
            {
                FinishCrystal();
                canMove = false;
            }
               
        }

    }

    public void FinishCrystal()
    {
        if (canExplode)
        {
            if (canGrow)
                transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);

            anim.SetTrigger("Explode");
            
        }
           
        else
            SelfDestroy();
    }

    public void SelfDestroy() => Destroy(gameObject);

    private void AnimationExplodeEvent()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cd.radius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
                player.stats.DoDamage(hit.GetComponent<CharacterStats>());
        }

    }
}
