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
            Debug.Log("û���㹻�ļ���������ü���");
            return false;
        }
        playerAbility = playerAbility - _price;
        return true;
    }
}
