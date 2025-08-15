using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieFromSkyController : MonoBehaviour
{
    [SerializeField] private GameObject ChangQiang;
    private float downSpeed;
    private int currentTarget;
    private int randomIndex;
   
    [SerializeField]private int weaponAmount ; 
    public List<Transform> target = new List<Transform>();
     
   

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetUpTargetForChangQiang();

        


       
    }
   

    public void SetUpDeatails(float _downSpeed, int _weaweaponAmount)
    {
        downSpeed = _downSpeed;
        weaponAmount = _weaweaponAmount;
        
    }
  

    private void SetUpTargetForChangQiang()
    {

        target.RemoveAll(t => t == null || !t.gameObject.activeInHierarchy);

        if (target.Count <= 0)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 500);

            foreach (var hit in colliders)
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                if (enemy != null && enemy.gameObject.activeInHierarchy)
                {
                    target.Add(hit.transform);
                }
            }
        }

    }

   

    public void SpawnItemB()
    {
        SetUpTargetForChangQiang();
        target.RemoveAll(t => t == null);

        if (target.Count <= 0)
            return;


        int randomIndex = Random.Range(0, target.Count);
        Transform selectedTarget = target[randomIndex];
        Debug.Log(target.Count);
        Debug.Log(randomIndex);
        
        Vector3 spawnPos = new Vector3(selectedTarget.position.x, selectedTarget.position.y+8,0);
        GameObject weapon = Instantiate(ChangQiang, spawnPos, Quaternion.identity);
        DieFromSkyWeapon weaponSpeed = weapon.GetComponent<DieFromSkyWeapon>();
        weaponSpeed.SetUpWeaponSpeed(downSpeed);

        PlayerManager.instance.player.AssignNewshengChang(weapon);
        
        DieFromSkyWeapon newdieFromSkyWeapon = weapon.GetComponent<DieFromSkyWeapon>();

  
      

        if (newdieFromSkyWeapon != null)
        {
            newdieFromSkyWeapon.Initialize(selectedTarget);
        }
    }

   public void StartCreatWeapon()
    {

       

        StartCoroutine("CreateWeapon");
    }

    private  IEnumerator CreateWeapon()
    {
       

      

        while ( weaponAmount>0)
        {
            if (!PlayerManager.instance.player.shengQinang)
            {
                SpawnItemB();
                weaponAmount--;

            }
           
            
            yield return new WaitForSeconds(1.7f);
           

        }
    }




}




