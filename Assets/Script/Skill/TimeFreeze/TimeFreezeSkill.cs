using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreezeSkill : Skill
{

   [SerializeField] private GameObject TimeFreezePrefab;
   [SerializeField] private float maxSize;
   [SerializeField] private float growSpeed;
   [SerializeField] private float shrinkSpeed;
    [SerializeField] private float timeFreezeDuration;
   public float amountOfAttacks;
   [SerializeField] public float cloneCooldown;
    TimeFreezeController currentTimeFreeze;

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        GameObject newTimeFreeze = Instantiate(TimeFreezePrefab,player.transform.position,Quaternion.identity);

        currentTimeFreeze = newTimeFreeze.GetComponent<TimeFreezeController>();
        currentTimeFreeze.SetUpTimeFreeze(maxSize,growSpeed,shrinkSpeed,amountOfAttacks,cloneCooldown,timeFreezeDuration); 

    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public bool SkillCompleted()
    {
        if (!currentTimeFreeze)
            return false;

        if (currentTimeFreeze.playerCanExitState)
        {
            currentTimeFreeze = null;
            return true;
        }

        return false;
    }
   
}
