using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShop : MonoBehaviour
{
    public InventoryBag_SO shopData;
    private bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseShop();
        }

        if (!isOpen&&Input.GetKeyDown(KeyCode.P))
            OpenShop();
    }

    public void OpenShop()
    {
        

        isOpen = true;
       
        EventHandler.CallBagOpenEvent(SlotType.NpcBag, shopData);
    }


    public void CloseShop()
    {
       

        isOpen = false;
        EventHandler.CallBagCloseEvent(SlotType.NpcBag, shopData);
    }

}
