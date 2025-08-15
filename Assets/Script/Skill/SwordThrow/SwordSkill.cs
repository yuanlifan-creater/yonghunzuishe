using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkill : Skill
{
    
    
   

    public WeaponType weaponType=WeaponType.changqiang;
    [Header("回弹剑信息")]
    [SerializeField] private SkillTreeSlot bounceUnLockButton;
    [SerializeField] private int amountOfBounce;
    [SerializeField] private float bounceGravity;
    [SerializeField] private float bounceSpeed;
    public bool bounceUnLock;


    [Header("穿刺枪信息")]
    [SerializeField] private SkillTreeSlot pierceUnLockButton;
    [SerializeField] private int pierceAmount;
    [SerializeField] private float pierceGravity;
    public bool pierceUnLock;


    [Header("技能信息")] 
    [SerializeField] private SkillTreeSlot swordUnLockButton;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float swordGravity;
    [SerializeField] private float freezeTimeDuration;
    [SerializeField] private float returnSpped;
    public bool swordUnLocked;

    private Vector2 finalDir;
    [Header("目标点")]
    [SerializeField] private int numberOfbots;
    [SerializeField] private float spaceBeetwenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;
    private GameObject[] dots;

    [Header("进阶技能信息")]
    [SerializeField] private SkillTreeSlot timeStopUnlockButton;
    public bool timeStopUnlocked;

    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();
        GenerateDots();

        swordUnLockButton.OnUnlocked.AddListener(UnlockSword);
        timeStopUnlockButton.OnUnlocked.AddListener(UnlockTimeStop);
        pierceUnLockButton.OnUnlocked.AddListener(UnlockPierce);
        bounceUnLockButton.OnUnlocked.AddListener(UnlockBounce);



    }
    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBeetwenDots);
            }
        }

        SetUpGravity();

    }
    private void SetUpGravity()
    {
        switch (weaponType)
        {
            case WeaponType.sword:
                swordGravity = bounceGravity;
                break;
            case WeaponType.changqiang:
                swordGravity = pierceGravity;
                break;
        }

    }


    private void UnlockTimeStop()
    {
        timeStopUnlocked = true;
    }

    private void UnlockSword()
    {
        swordUnLocked = true;
    }

    private void UnlockBounce()
    {
       bounceUnLock = true;
    }

    private void UnlockPierce()
    {
        pierceUnLock = true;
    }

    public void CreateSword()
    {
       GameObject newSword = Instantiate(weaponPrefab, player.transform.position, transform.rotation);
        SwordSkillController newSwordScript = newSword.GetComponent<SwordSkillController>();

        if (weaponType == WeaponType.sword)
        {
            
            newSwordScript.SetUpBounce(true,amountOfBounce,bounceSpeed);
        }else if (weaponType == WeaponType.changqiang)
        {
            newSwordScript.SetUpPierce(pierceAmount);
        }

        newSwordScript.SetupSword(finalDir, swordGravity, player, freezeTimeDuration, returnSpped) ;

        player.AssignNewSword(newSword);
        
        DotsActive(false);
    }

    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }
    public void DotsActive(bool isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(isActive);
        }
    }
    private void GenerateDots()
    {
        dots = new GameObject[numberOfbots];
        for (int i = 0; i < numberOfbots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPosition(float t)
    {
        Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) * t + .5f*(Physics2D.gravity * swordGravity) * (t * t);

        return position;
        
    }

}
