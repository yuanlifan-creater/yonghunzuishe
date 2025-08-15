using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;
    // Start is called before the first frame update

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    protected override void Start()
    {
        base.Start();
    }

    
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
        player.Die();
    }


}
