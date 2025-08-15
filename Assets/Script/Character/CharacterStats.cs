using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat strength;
    public Stat damage;
    public Stat maxHealthy;
    public int currentHealthy;
    private Entity_FX fx;
    public bool isDead { get; protected set; }
    public System.Action onHealthChanged;

    // Start is called before the first frame update
   protected virtual void Start()
    {
        currentHealthy = GetMaxHealthValue();
        fx = GetComponent<Entity_FX>();
        //onHealthChanged();
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        int totalDamage = damage.GetValue() + strength.GetValue();
        _targetStats.takeDamage(totalDamage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void takeDamage(int _damage)
    {
        DecreaseHealthBy(_damage);
        GetComponent<Entity>().DamageEffect();
        fx.StartCoroutine("FlashFx");
        if (currentHealthy < 0)
            Die();

       
    }

    protected virtual void Die()
    {
        isDead = true;
    }

    protected virtual void DecreaseHealthBy(int _damage)
    {
        currentHealthy -= _damage;
        if (onHealthChanged != null)
            onHealthChanged();
    }

    public int GetMaxHealthValue()
    {
        return maxHealthy.GetValue();
    }

}
