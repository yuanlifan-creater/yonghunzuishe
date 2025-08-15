using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
   
    public DashSkill dash { get; private set; }
    public CloneSkill clone { get; private set; }
    public SwordSkill sword { get; private set; }
    public TimeFreezeSkill timeFreeze { get; private set; }
    public CryStalSkill CryStal { get; private set; }
    public WanWuMuSkill wanWuMu { get; private set; }
    public ShunShiZhanSkill shunShiZhan { get; private set; }

    public DieFromSkill dieFromSkill { get; private set; }
    private void Awake()
    {

      if (instance != null)
      {
          Destroy(instance.gameObject);
          instance = this;
      }
      else 
          instance = this;

        
    }
    // Start is called before the first frame update
    void Start()
    {
        dash = GetComponent<DashSkill>();
        clone = GetComponent<CloneSkill>();
        sword = GetComponent<SwordSkill>();
        timeFreeze = GetComponent<TimeFreezeSkill>();
        wanWuMu = GetComponent<WanWuMuSkill>();
        CryStal = GetComponent<CryStalSkill>();
        shunShiZhan = GetComponent<ShunShiZhanSkill>();
        dieFromSkill = GetComponent<DieFromSkill>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

}
