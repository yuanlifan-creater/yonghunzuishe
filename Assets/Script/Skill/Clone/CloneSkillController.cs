using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    [SerializeField] private float cloneLoosingSpeed;
    private Animator anim;
    private float cloneTimer;
    private SpriteRenderer sr;
    private Transform closestEnemy;
    private Player player;

    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = 1.7f;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cloneTimer -= Time.deltaTime;
        if(cloneTimer<0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * cloneLoosingSpeed));

            if (sr.color.a <= 0)
                Destroy(gameObject);
        }
    }

    public void SetUpClone(Transform _newTransform, float _cloneDuration ,bool canAttack,Vector3 _offset,Transform _closestEnemy,Player _player)
    {
        if (canAttack)
            anim.SetInteger("attackCounter", Random.Range(1, 4));
        player = _player;
        transform.position = _newTransform.position+_offset;
        cloneTimer = _cloneDuration;
        closestEnemy = _closestEnemy;
        FaceClosestTarget();
    }

    private void AnimationTrigger()
    {
        cloneTimer = -.1f;
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                
                player.stats.DoDamage(hit.GetComponent<CharacterStats>());
            }
              
        }
    }

    private void FaceClosestTarget()
    {
        

        if (closestEnemy != null)
        {
            if (transform.position.x > closestEnemy.position.x)
                transform.Rotate(0, 180, 0);
        }


    }


}
