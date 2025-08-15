using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieFromSkyWeapon : MonoBehaviour
{

    private float moveSpeed ;
    [SerializeField] private float upSpeed;
    private float upTime = 1.5f;
    private Transform target;
    private Player player ;
    public void Initialize(Transform target)
    {
        this.target = target;
        transform.Rotate(0, 0, 180);
    }

    public void SetUpWeaponSpeed(float _moveSpeed)
    {
        moveSpeed = _moveSpeed;
    }

    private void Start()
    {
        player = PlayerManager.instance.player;
    }

    private void Update()
    {
        upTime -= Time.deltaTime;

        if (target == null)
        {
            
            Destroy(gameObject);
            return;
        }
        if (upTime >= 0)
        {
            transform.Translate(Vector2.down * upSpeed * Time.deltaTime);
            float newX = Mathf.MoveTowards(
               transform.position.x,
               target.position.x,
               3 * Time.deltaTime
           );
            transform.position = new Vector2(newX, transform.position.y);

        }
           
        else
        {
            transform.position = Vector2.MoveTowards(transform.position,
        target.position, moveSpeed * Time.deltaTime);
        }
        
        if(target==null)
            PlayerManager.instance.player.DestroyTheShengQiang();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            player.stats.DoDamage(collision.GetComponent<CharacterStats>());
            Destroy(gameObject);
            PlayerManager.instance.player.DestroyTheShengQiang();

        }
    }

   
}


    
  



