using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShunShiZhanSkill : Skill
{
    [SerializeField] private float freezeTimeDuration;
    [SerializeField] private GameObject ZhanJi;
    [SerializeField] private float CreateZhanJiAmount;
    public float ShunShiZhanSpeed;
    public float shunShiZhanDuration;
    [SerializeField] private bool startZhanJi;

    private float randomAngle;
    private float randomX;
    private Vector3 spawnPosition;
    private Quaternion randomRotation;
    
    private bool isPlayerFacingRight;
    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();
        
    }

    protected override void Update()
    {
        base.Update();
        if(startZhanJi)
            shunShiZhanDuration -= Time.deltaTime;
        else if (shunShiZhanDuration <= 0)
        {
            startZhanJi = false;
            shunShiZhanDuration = 5;
        }

    }

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();


       



        StartCoroutine("shunShiZhan");



        



       
    }

    private IEnumerator shunShiZhan()
    {
        startZhanJi = true;

        if (PlayerManager.instance.player.facingDir == 1)
        {
            isPlayerFacingRight=true;
        }
        else 
        {
            isPlayerFacingRight = false;
        }
       

        Vector3 ZhanJiFanwei = player.transform.position;

        while (shunShiZhanDuration>=0)
        {
            
            randomAngle = Random.Range(-45f, 0f);
           float randomAngleZstart = Random.Range(-180f, -135f);
            float finalrandomAngle = Random.Range(0f, 1f);
            if(finalrandomAngle <= 0.5f)
                randomRotation = Quaternion.Euler(0f, 0, randomAngle);
            else 
                randomRotation = Quaternion.Euler(0f, 0, randomAngleZstart);


           
            if (isPlayerFacingRight)
            {
                randomX = Random.Range(ZhanJiFanwei.x - 10f, ZhanJiFanwei.x);
            }
            else
            {
                randomX = Random.Range(ZhanJiFanwei.x + 10f, ZhanJiFanwei.x);
            }

            spawnPosition = new Vector3(randomX, ZhanJiFanwei.y+3.5f, 0);
            GameObject newZhanJi = Instantiate(ZhanJi, spawnPosition, randomRotation);
           
            ShunShiZhanController newshunShiZhan = newZhanJi.GetComponent<ShunShiZhanController>();
            newshunShiZhan.SetupShunShiZhan(freezeTimeDuration,player);

            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(shunShiZhanDuration);

        startZhanJi = false;
    }





}
