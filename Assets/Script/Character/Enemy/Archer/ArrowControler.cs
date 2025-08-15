using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControler : MonoBehaviour
{
    [SerializeField] private string targetName = "Player";
    [SerializeField] private float damage;
    [SerializeField] private float xVelocity;
    [SerializeField] private bool canMove;
    [SerializeField] private bool fliped;
    [SerializeField] private float flyTime;
   
    private Rigidbody2D rb;

    private CharacterStats myStats;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    

    private void Update()
    {
        flyTime -= Time.deltaTime;

        if(canMove&&flyTime>0)
        rb.velocity = new Vector2(xVelocity, 0);
        else if(flyTime<=0)
            rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }

    public void SetUpArrow(float _speed,CharacterStats _myStats,float _flyTime)
    {
        
        xVelocity = _speed;
        myStats = _myStats;
        flyTime = _flyTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(targetName))
        {
           myStats.DoDamage(collision.GetComponent<CharacterStats>());

            StuckInto(collision);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            StuckInto(collision);

       
    }
   private void StuckInto(Collider2D collision)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        canMove = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = collision.transform;
        Destroy(gameObject, 2f);
    }

    public void FlipArrow()
    {
        if (fliped)
            return;

        xVelocity = xVelocity * -1;
        fliped = true;
        transform.Rotate(0, 180, 0);
        targetName = "Enemy";
    }



}
