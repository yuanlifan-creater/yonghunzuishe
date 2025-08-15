using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreezeController : MonoBehaviour
{
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keyCodeList;

    private float maxSize;
    private float growSpeed;
    private bool canGrow=true;
    private float shrinkSpeed;
    private bool canShrink;
    private float timeFreezeTimer;

    public float amountOfAttacks = 4;
    private float cloneAttackTimer;
    public float cloneAttackCooldown = .3f;
    private bool cloneAttackReleased;

    public bool playerCanExitState { get; private set; }

    public bool canCreateHotKey=true;
    private List<Transform> target = new List<Transform>();
    private List<GameObject> createHotKey = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
       

    }

    public void SetUpTimeFreeze(float _maxSize, float _growSpeed, float _shrinkSpeed, float _amountOfAttack, float _cloneAttackCooldown, float _timeFreezeTimer)
    {
        maxSize = _maxSize;
        growSpeed = _growSpeed;
        amountOfAttacks = _amountOfAttack;
        cloneAttackCooldown = _cloneAttackCooldown;
        shrinkSpeed = _shrinkSpeed;
        timeFreezeTimer = _timeFreezeTimer;
          
    }
    // Update is called once per frame
    void Update()
    {
        cloneAttackTimer -= Time.deltaTime;
        timeFreezeTimer-= Time.deltaTime; ;

        if (timeFreezeTimer < 0)
        {
            timeFreezeTimer = Mathf.Infinity;
            if (target.Count > 0)
                ReleaseCloneAttack();
            else
                FinishTimeFreeze();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReleaseCloneAttack();

        }

        CloneAttackLogic();

        if (canGrow && !canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);


           
        }
        if (canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);
            if (transform.lossyScale.x < 0)
                Destroy(gameObject);

            
        }
    }

    private void ReleaseCloneAttack()
    {
        if (target.Count < 0)
            return;

        DestroyHotKey();
        cloneAttackReleased = true;
        canCreateHotKey = false;
        PlayerManager.instance.player.currentRenter.color = new Color(1, 1, 1, 0);
    }

    private void CloneAttackLogic()
    {
        if (cloneAttackTimer < 0 && cloneAttackReleased&&amountOfAttacks>0)
        {
            cloneAttackTimer = cloneAttackCooldown;
            int randomIndex = Random.Range(0, target.Count);

            float offset;
            if (Random.Range(0, 180) > 50)
                offset = 1.5f;
            else
                offset = -1.5f;

            SkillManager.instance.clone.CreateClone(target[randomIndex], new Vector3(offset, 0));
            amountOfAttacks--;

        }

        if (amountOfAttacks <= 0)
        {
            Invoke("FinishTimeFreeze", .5f);
        }
    }

    private void FinishTimeFreeze()
    {
        DestroyHotKey();
        playerCanExitState = true;
     
        canShrink = true;
        cloneAttackReleased = false;
        PlayerManager.instance.player.currentRenter.color = new Color(1, 1, 1, 255);
    }

    private void DestroyHotKey()
    {
        if (createHotKey.Count <= 0)
            return;

        for (int i = 0; i < createHotKey.Count; i++)
        {
            Destroy(createHotKey[i]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTimer(true);
            CreateHotKey(collision);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
            collision.GetComponent<Enemy>()?.FreezeTimer(false);
    }

    private void CreateHotKey(Collider2D collision)
    {
        if (keyCodeList.Count <= 0)
            return;

        if (!canCreateHotKey)
            return;

        GameObject newHotKey = Instantiate(hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);
        createHotKey.Add(newHotKey);

        KeyCode choosenKey = keyCodeList[Random.Range(0, keyCodeList.Count)];
        keyCodeList.Remove(choosenKey);

        TimeFreezeHotKeyController newHotKeyScript = newHotKey.GetComponent<TimeFreezeHotKeyController>();
        newHotKeyScript.SetupHotKey(choosenKey, collision.transform, this);
    }

    public void AddEnemyToList(Transform _enemyTransform) => target.Add(_enemyTransform);
}
