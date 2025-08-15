using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    private ItemDrop myDropSystem;



    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        myDropSystem = GetComponent<ItemDrop>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void takeDamage(int _damage)
    {
        base.takeDamage(_damage);
        
    }
    protected override void Die()
    {
        base.Die();
        enemy.Die();
        myDropSystem.DropItem();
      
    }

   

}
