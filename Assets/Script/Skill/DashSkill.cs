using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashSkill : Skill
{
    [Header("³å´Ì")]
    [SerializeField] private SkillTreeSlot dashUnlockButton;
    public bool dashUnlocked;

    [Header("³å´Ì¹¥»÷")]
    [SerializeField] private SkillTreeSlot cloneOnDashUnlockButton;
    public bool cloneOnDashUnlocked;



    private void Awake()
    {
        
    }
    protected override void Start()
    {
        base.Start();
        dashUnlockButton.OnUnlocked.AddListener(UnlockDash);
        cloneOnDashUnlockButton.OnUnlocked.AddListener(UnLockCloneOnDash);

    }
    public override void UseSkill()
    {
        base.UseSkill();
    }

   public void UnlockDash()
    {
        
        
            dashUnlocked = true;
        
       
    }

    private void UnLockCloneOnDash()
    {
        
        cloneOnDashUnlocked = true;
    }

    public void cloneDashStart()
    {
        if (cloneOnDashUnlocked)
            SkillManager.instance.clone.CreateClone(player.transform, Vector3.zero);
    }

   
}
