using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(Entity_FX))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Enemy : Entity, IPoolableEnemy
{
    private ObjectPool<GameObject> _pool;
    [SerializeField]protected LayerMask whatIsPlayer;
    [Header("移动信息")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;
    private float defaultSpeed;

    [Header("攻击信息")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector]public float lastTimeAttacked;
    [SerializeField] protected Transform playerCheck;
    [SerializeField] protected float playerCheckDistance;

    [Header("眩晕信息")]
    public Vector2 stunDirection;
    public float   stunDuration;
    protected bool CanStun;
    public GameObject counterImage;
   

    public EnemyStateMachine stateMachine { get; private set; }
    public string lastAnimBoolName { get; private set; }

    


    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        defaultSpeed = moveSpeed;
    }
    // Start is called before the first frame update


    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
    public void InitializePool(ObjectPool<GameObject> pool)
    {
        _pool = pool;

    }
    public virtual void AssignLastAnimName(string _animBoolName)
    {
        lastAnimBoolName = _animBoolName;
    }

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, playerCheckDistance, whatIsPlayer);

    public virtual void OpenCounterAttackWindow()
    {
        CanStun = true;
        
    }

    public virtual void CloseCounterAttackWindow()
    {
        CanStun = false;
       
    }

    public virtual bool CanbeStunned()
    {
        if (CanStun)
        {
            CloseCounterAttackWindow();
            return true;
        }

        return false;
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + playerCheckDistance * facingDir, playerCheck.position.y));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }
    public virtual void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();
    public virtual void AnimationAttackTrigger()
    {

    }
    protected IEnumerator Startattle()
    {
        counterImage.SetActive(true);

        yield return new WaitForSeconds(1f);
        counterImage.SetActive(false);
    }

    public virtual void FreezeTimer(bool timeFozen)
    {
        if (timeFozen)
        {
            moveSpeed = 0;
            anim.speed = 0;
        }
        else
        {
            moveSpeed = defaultSpeed;
            anim.speed = 1;
        }
    }

    protected virtual IEnumerator FreezeTimeFor(float _seconds)
    {
        FreezeTimer(true);

        yield return new WaitForSeconds(_seconds);

        FreezeTimer(false);
    }

    public void Release()
    {
        _pool.Release(gameObject); // 回收自身到对象池

    }


}
