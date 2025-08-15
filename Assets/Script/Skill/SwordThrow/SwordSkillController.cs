using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    
    private float returnSpped;
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;
    private float freezeTimeDuration;

    private bool canRotate=true;
    private bool isReturn;

    [Header("穿刺枪信息")]
    [SerializeField] private float pierceAmount;

    [Header("剑信息")]
    private float bounceSpeed;
    private bool isBouncing ;
    private int bounceAmount ;
    private List<Transform> enemyTarget;
    private int targetIndex;
    private void Awake()
    {
        
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        

    }
    public void SetupSword(Vector2 dir, float gravitScale, Player _player, float _freezeTimeDuration, float _returnSpeed)
    {
        player = _player;
        rb.velocity = dir;
        rb.gravityScale = gravitScale;
        freezeTimeDuration = _freezeTimeDuration;
         returnSpped = _returnSpeed;
        if(pierceAmount<=0&&SkillManager.instance.sword.weaponType!=WeaponType.changqiang)
        anim.SetBool("Rotate", true);

        Invoke("ReturnSword", 7f);
    }

    public void SetUpBounce(bool _isBounce, int _amountOfBounce,float _bounceSpeed)
    {
        isBouncing = _isBounce;
        bounceAmount = _amountOfBounce;
        bounceSpeed = _bounceSpeed;
        enemyTarget = new List<Transform>();
    }

    public void SetUpPierce(int _pierceAmount)
    {
        pierceAmount = _pierceAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (canRotate)
            transform.right = rb.velocity;

        if (isReturn)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpped * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) < 1)
                player.CatchTheSword();
        }

        BounceSwordLogic();
    }

    private void BounceSwordLogic()
    {
        if (isBouncing && enemyTarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bounceSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < .1f)
            {
                weaponSkillDamage(enemyTarget[targetIndex].GetComponent<Enemy>());
                
                targetIndex++;
                bounceAmount--;

                if (bounceAmount <= 0)
                {
                    isBouncing = false;
                    isReturn = true;
                }

                if (targetIndex >= enemyTarget.Count)
                    targetIndex = 0;
            }
        }
    }

    
    public void ReturnSword()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = null;
        isReturn = true;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturn)
            return;

        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            weaponSkillDamage(enemy);
        }

        SetUpTargetForSword(collision);

        StuckInto(collision);

        

    }

    private void weaponSkillDamage(Enemy enemy)
    {
        EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();

        player.stats.DoDamage(enemyStats);



        enemy.DamageEffect();

        if(player.skill.sword.timeStopUnlocked)
        enemy.StartCoroutine("FreezeTimeFor", freezeTimeDuration);
    }

    private void SetUpTargetForSword(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            if (isBouncing && enemyTarget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                    {
                        enemyTarget.Add(hit.transform);

                    }

                }
            }
        }
    }

    private void StuckInto(Collider2D collision)
    {
        if (pierceAmount > 0 && collision.GetComponent<Enemy>() != null)
        {
            pierceAmount --;
            return;
        }
           
       
        canRotate = false;
        cd.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (isBouncing && enemyTarget.Count>0)
            return;

        anim.SetBool("Rotate", false);

        transform.parent = collision.transform;
    }
}
