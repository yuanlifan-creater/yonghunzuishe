using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieFromSkill : Skill
{
    [SerializeField]private GameObject item;
    [SerializeField] private float downSpeed;
    private int defaultAmount;
    public int weaponAmount;
    


    protected override void Start()
    {
        base.Start();

        defaultAmount = weaponAmount;
    }
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        LetDie();
    }



   


    private void LetDie()
    {



        weaponAmount = defaultAmount;

        GameObject transformPerfence = Instantiate(item, player.transform.position, Quaternion.identity);
        DieFromSkyController newDie = transformPerfence.GetComponent<DieFromSkyController>();

        newDie.SetUpDeatails(downSpeed, weaponAmount);
        


        StartCoroutine("CanCreateWeapon");
        newDie.StartCreatWeapon();
        
       

    }

    protected override void Update()
    {
        base.Update();
      


    }

    private IEnumerator CanCreateWeapon()
    {
       
        while (weaponAmount > 0)
        {
            weaponAmount--;
            yield return new WaitForSeconds(1.7f);
        }
    }

    // Start is called before the first frame update
    
}
