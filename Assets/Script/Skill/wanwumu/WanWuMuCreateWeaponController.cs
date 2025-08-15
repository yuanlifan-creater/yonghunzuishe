using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanWuMuCreateWeaponController : MonoBehaviour
{
    [SerializeField]private Sprite[] wanWuMuSprite;
    private SpriteRenderer currentSprite;
    private Rigidbody2D rb;
    private float weaponSpeed;
    public List<Transform> target = new List<Transform>();
    private float weaponMoveSpeed;
    private int currentTarget;
    private int randomIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetUpWeapon(float _weaponMoveSpeed)
    {
        currentSprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        int weaponSprite = Random.Range(0, 3);     
        currentSprite.sprite = wanWuMuSprite[weaponSprite];
        weaponMoveSpeed = _weaponMoveSpeed;
       
        Debug.Log(weaponSprite);
    }

    public void weaponDamage(Enemy enemy)
    {
        enemy.DamageEffect();
    }

    // Update is called once per frame
    void Update()
    {
        SetUpTargetForWWM();
        ReleaseWeaponAttack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            weaponDamage(enemy);
            Destroy(gameObject);

        }
    }

    private void SetUpTargetForWWM()
    {
        
        if (target.Count <= 0)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 500);
       
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() != null)
                {
                    target.Add(hit.transform);
                }

            }
        }
    }
    private void ReleaseWeaponAttack()
    {
        if (target.Count < 0)
            return;
        if (target.Count > 0)
        {
             
            while (currentTarget == 0)
            {
                randomIndex = Random.Range(0, target.Count);
            }
            
            currentTarget = randomIndex;
            transform.position = Vector2.MoveTowards(transform.position,
                target[randomIndex].transform.position, weaponMoveSpeed * Time.deltaTime);


            if (Vector2.Distance(transform.position, target[randomIndex].transform.position) < .1f)
            {
                weaponDamage(target[randomIndex].GetComponent<Enemy>());
            }

        }

    }

}
