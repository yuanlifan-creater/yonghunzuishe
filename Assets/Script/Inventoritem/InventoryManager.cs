using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yhzs.Inventory { 

public class InventoryManager : Singletion<InventoryManager>
{
        [Header("物品数据")]
        public ItemList_SO itemDataList_SO;
        [Header("背包数据")]
        public InventoryBag_SO playerBag;
        public InventoryBag_SO NPCBag;

        [Header("商人提示")]
        [SerializeField] private GameObject[] NotEnoughMoneyAndItem;
        
        private Player player;
        private void Start()
        {
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
            player = PlayerManager.instance.player;
        }
        public ItemDetails GetItemDetails(int ID)
        {
            return itemDataList_SO.itemDetails.Find(i => i.itemID == ID);
        }

      public void AddItem(Item item, bool toDestory)
        {
            var index = GetItemIndexInBag(item.itemID);

            AddItemAtIndex(item.itemID, index, 1);
            if (toDestory)
            {
                Destroy(item.gameObject);
            }
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player,playerBag.itemList);
        }

       private bool CheckBagCapacity()
        {
            

            for (int i = 0; i < playerBag.itemList.Count; i++)
            {
                if (playerBag.itemList[i].itemID == 0)
                    return true;
            }
            return false;
        }

        private int GetItemIndexInBag(int ID)
        {
            for (int i = 0; i < playerBag.itemList.Count; i++)
            {
                if (playerBag.itemList[i].itemID == ID)
                    return i;
            }
            return -1;
        }

        private void AddItemAtIndex(int ID,int index,int amount)
        {
            if (index == -1&&CheckBagCapacity())
            {
                var item = new InventoryItem { itemID = ID, itemAmount = amount };
                for (int i = 0; i < playerBag.itemList.Count; i++)
                {
                    if (playerBag.itemList[i].itemID == 0)
                    {
                        playerBag.itemList[i] = item;
                        break;

                    }
                }
            }
            else
            {
                int currentAmount = playerBag.itemList[index].itemAmount + amount;
                var item = new InventoryItem { itemID = ID, itemAmount = currentAmount };
                playerBag.itemList[index] = item;

            }
        }
        private void ReMoveItem(int ID,int removeAmount)
        {
            var index = GetItemIndexInBag(ID);

                var amount = NPCBag.itemList[index].itemAmount - removeAmount;
                var item = new InventoryItem { itemID = ID, itemAmount = amount };
                NPCBag.itemList[index] = item;
            
            
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Shop, NPCBag.itemList);
        }
        public void TradeItem(ItemDetails itemDetails,int amount)
        {
            int cost = itemDetails.itemPrice * amount;
            int index = GetItemIndexInBag(itemDetails.itemID);

            if(NPCBag.itemList[index].itemAmount <amount)
            {
              
                if (NotEnoughMoneyAndItem == null || NotEnoughMoneyAndItem.Length == 0)
                {
                    Debug.LogError("目标数组为空或未分配！");
                    return;
                }

               
                foreach (GameObject item in NotEnoughMoneyAndItem)
                {
                    if (item != null) item.SetActive(false);
                }

                
                int randomIndex = Random.Range(0, 2);

                
                if (NotEnoughMoneyAndItem[randomIndex] != null)
                {
                    NotEnoughMoneyAndItem[randomIndex].SetActive(true);
                   
                }
                return;
            }
            else if(NPCBag.itemList[index].itemAmount > amount)
            {

                if (player.playerMoney - cost >= 0)
                {
                    Debug.Log(NPCBag.itemList[index].itemAmount);
                    ReMoveItem(itemDetails.itemID, amount);
                    AddItemAtIndex(itemDetails.itemID, index, amount);
                    player.playerMoney -= cost;
                }
                else
                {
                    if (NotEnoughMoneyAndItem == null || NotEnoughMoneyAndItem.Length == 0)
                    {
                        Debug.LogError("目标数组为空或未分配！");
                        return;
                    }


                    foreach (GameObject item in NotEnoughMoneyAndItem)
                    {
                        if (item != null) item.SetActive(false);
                    }


                    int randomIndex = Random.Range(2, 4);


                    if (NotEnoughMoneyAndItem[randomIndex] != null)
                    {
                        NotEnoughMoneyAndItem[randomIndex].SetActive(true);

                    }
                }
            }


            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }

}


}