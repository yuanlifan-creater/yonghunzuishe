using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{
    [SerializeField] private GameObject swordClonePrefab;
    [SerializeField] private GameObject qcClonePrefab;

    [SerializeField] private bool canAttack;
    [SerializeField] private float cloneDuration;

    [SerializeField] private bool createCloneStart;
    private GameObject newClone;
    // Start is called before the first frame update
   

    public void CreateClone(Transform _clonePosition,Vector3 _offset)
    {
        

        if(PlayerManager.instance.player.swordState)
        newClone = Instantiate(swordClonePrefab);
        else if(PlayerManager.instance.player.qcState)
        newClone = Instantiate(qcClonePrefab);


        newClone.GetComponent<CloneSkillController>().SetUpClone(_clonePosition, cloneDuration,canAttack,_offset,FindClosestEnemy(newClone.transform),player);
    }

    

}
