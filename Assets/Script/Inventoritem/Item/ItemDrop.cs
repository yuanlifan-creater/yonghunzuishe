using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private GameObject dropMoney;
    [SerializeField] private GameObject a;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropItem()
    {
        GameObject b = Instantiate(a, transform.position, Quaternion.identity);
        for (int i = 0; i < 8; i++)
        {
            GameObject newDrop = Instantiate(dropMoney, transform.position, Quaternion.identity);
            Money money = newDrop.GetComponent<Money>();

            float randomValue = Random.value;
            int selectedIndex = 0;

            if (randomValue < 0.6f)       // 60% 概率
            {
                selectedIndex = 0;
            }
            else if (randomValue < 0.9f)  // 30% 概率（累计到90%）
            {
                selectedIndex = 1;
            }
            else                          // 10% 概率（剩余10%）
            {
                selectedIndex = 2;
            }
            money.ManualInitialize(selectedIndex);
        }
       
        
    }
}
