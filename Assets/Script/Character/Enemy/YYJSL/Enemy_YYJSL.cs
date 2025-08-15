using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_YYJSL : Enemy
{
    public bool BossFightBegan;

    [Header("传送细节")]
    [SerializeField] private BoxCollider2D arena;
    [SerializeField] private Vector2 surroundingCheck;
    public float chanceToTeleport;
    public float defultChanceToTeleport;

    [Header("技能细节")]
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private float spellStateCooldown;
    public int amountOfSpells;
    public float spellCoolDown;
    public  float lastTimeCast;

   

    public YyjslIdleState idleState { get; private set; }
   
    public YyjslBattleState battleState { get; private set; }
    public YyjslAttackState attackState { get; private set; }
    public YyjslDeadState deadState { get; private set; }
    public YyjslTelePortState telePortState { get; private set; }
    public YyjslSpellCastState castState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        

        idleState = new YyjslIdleState(this, stateMachine, "Idle", this);
        telePortState = new YyjslTelePortState(this, stateMachine, "TelePort", this);
        battleState = new YyjslBattleState(this, stateMachine, "run", this);
        attackState = new YyjslAttackState(this, stateMachine, "Attack", this);
        castState = new YyjslSpellCastState(this, stateMachine, "Cast", this);

        deadState = new YyjslDeadState(this, stateMachine, "die", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }

    public void FindPosition()
    {
        float x = Random.Range(arena.bounds.min.x + 3, arena.bounds.max.x - 3) ;
        float y = Random.Range(arena.bounds.min.y + 3, arena.bounds.max.y - 3);
        transform.position = new Vector3(x, y);
        transform.position = new Vector3(transform.position.x, transform.position.y - GroundBelow().distance + (cd.size.y / 2));

        if (!GroundBelow() || SomethingIsAround())
        {
            FindPosition();
        }

    }

    public void CastSpell()
    {
        Player player = PlayerManager.instance.player;
        float xOffset = 0;
       
        if (player.rb.velocity.x != 0)
            xOffset = player.facingDir * 3;

        Vector3 spellPosition = new Vector3(player.transform.position.x  +xOffset, player.transform.position.y + 2);
       


        GameObject newSpell = Instantiate(spellPrefab, spellPosition, Quaternion.identity);
        newSpell.GetComponent<YyjslSpellCastController>().SetUpSpell(stats);
    }

    private RaycastHit2D GroundBelow() => Physics2D.Raycast(transform.position, Vector2.down, 100, whatIsGround);
    private bool SomethingIsAround() => Physics2D.BoxCast(transform.position, surroundingCheck, 0, Vector2.zero, 0, whatIsGround);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position, surroundingCheck);
            
    }

    public bool CanTeleport()
    {
        if (Random.Range(0, 100) <= chanceToTeleport)
        {
            chanceToTeleport = defultChanceToTeleport;
            return true;
        }
            
        else
            return false;
    }

    public bool CanDoSpellCast()
    {
        if (Time.time >= lastTimeCast+spellCoolDown)
        {
            
            return true;
        }
        return false;
    }


}
