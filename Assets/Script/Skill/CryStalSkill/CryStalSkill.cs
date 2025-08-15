using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryStalSkill : Skill
{
    [SerializeField] private GameObject crystalPrefab;
    [SerializeField] private float cryStalDuration;
    private GameObject currentCryStal;
    [SerializeField] private SkillTreeSlot unlockCrystalButton;
    public bool cryStalUnlocked;

    [Header("±¨’®")]
    [SerializeField] private bool canExplode;
    [SerializeField]private bool canGrow;
    [SerializeField]private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private SkillTreeSlot unlockCrystalExplodeButton;

    [Header("“∆œÚµ–»À")]
    [SerializeField] private bool canMoveToEnemy;
    [SerializeField] private float moveSpeed;
    [SerializeField] private SkillTreeSlot unlockCrystalMoveButton;
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        if (!cryStalUnlocked)
            return;
        if (currentCryStal == null)
        {
            currentCryStal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
            CryStalSkillController currentCryStalScript = currentCryStal.GetComponent<CryStalSkillController>();
            currentCryStalScript.SetupCrystal(cryStalDuration, canExplode, canMoveToEnemy, moveSpeed, maxSize, growSpeed, canGrow,
                FindClosestEnemy(currentCryStal.transform),player); 
        }
            
        else
        {
            if (canMoveToEnemy)
                return;


            Vector2 playerPos = player.transform.position;

            player.transform.position = currentCryStal.transform.position;

            currentCryStal.transform.position = playerPos;
            currentCryStal.GetComponent<CryStalSkillController>()?.FinishCrystal();
        }
    }

    protected override void Start()
    {
        base.Start();
        unlockCrystalButton.OnUnlocked.AddListener(UnlockCrystal);
        unlockCrystalExplodeButton.OnUnlocked.AddListener(UnlockCrystalExplode);
        unlockCrystalMoveButton.OnUnlocked.AddListener(UnlockCrystalMove);
    }

    protected override void Update()
    {
        base.Update();
    }

    private void UnlockCrystal()
    {
        cryStalUnlocked = true;
    }

    private void UnlockCrystalExplode()
    {
        canExplode = true;

    }

    private void UnlockCrystalMove()
    {
        canMoveToEnemy = true;
    }

}
