using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanWuMuSkill : Skill
{
    [SerializeField] private GameObject wanWuMuPrefab;
    [SerializeField] private GameObject wanWuMuWeaponPrefab;
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float shrinkSpeed;
    public float wanWuMuDuration;
    [SerializeField] private float weaponMoveSpeed;
    [SerializeField] private float maxCanCreate;
    
    private int createCount = 0;

    WanWuMuController wanWuMu;
    WanWuMuCreateWeaponController wanWuMuWeapon;
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        GameObject attackPrefab = Instantiate(wanWuMuPrefab, player.transform.position, Quaternion.identity);
        wanWuMu =attackPrefab.GetComponent<WanWuMuController>();
        wanWuMu.GetComponent<WanWuMuController>().SetUpWanWuMu(maxSize, growSpeed, shrinkSpeed, wanWuMuDuration) ;
        
         
        StartCoroutine("SpawnObjectRoutine");

    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private IEnumerator SpawnObjectRoutine()
    {
        while (createCount < wanWuMuDuration) 
        {
            GameObject currentWeapon = Instantiate(wanWuMuWeaponPrefab, player.transform.position, Quaternion.identity);
            wanWuMuWeapon = currentWeapon.GetComponent<WanWuMuCreateWeaponController>();
            wanWuMuWeapon.SetUpWeapon(weaponMoveSpeed);
            createCount++; 
            yield return new WaitForSeconds(1f);
        }
        
    }

}
