using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Player player;
    public int playerAbility;
   


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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool HaveEnoughMoney(int _price)
    {
        if (_price > playerAbility)
        {
            Debug.Log("没有足够的记忆来回忆该技能");
            return false;
        }
        playerAbility = playerAbility - _price;
        return true;
    }
}
