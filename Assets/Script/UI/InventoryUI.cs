using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace yhzs.Inventory { 
public class InventoryUI : MonoBehaviour
{
   [SerializeField] private SlotUI[] playerSlots;
   [SerializeField] private GameObject bagUI;
        public ItemToolTip itemToolTip;
        private bool bagOpened;

    [Header("通用背包")]
    [SerializeField] private GameObject shopSlotPrefab;
    [SerializeField] private List<SlotUI> baseBagSlots;
    [SerializeField] private GameObject shop;

    [Header("商店")]
    public TradeUI tradeUI;

        public static InventoryUI instance;


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

        private void OnEnable()
        {
            EventHandler.UpdateInventoryUI += OnUpdateInventoryUI;
           
                EventHandler.BagOpenEvent += OnBagOpenEvent;
            
            EventHandler.BagCloseEvent += OnBagCloseEvent;
            EventHandler.ShowTradeUI += OnShowTradeUI;
                }
        private void OnDisable()
        {
            EventHandler.UpdateInventoryUI -= OnUpdateInventoryUI;
            
                EventHandler.BagOpenEvent -= OnBagOpenEvent;
            
            EventHandler.BagCloseEvent -= OnBagCloseEvent;
            EventHandler.ShowTradeUI -= OnShowTradeUI;
        }



       




        // Start is called before the first frame update
        void Start()
    {
            for (int i = 0; i < playerSlots.Length; i++)
            {
                playerSlots[i].slotIndex = i;
            }
            bagOpened = bagUI.activeInHierarchy;
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.B))
            {
                OpenBagUI();
            }
    }
        private void OnShowTradeUI(ItemDetails item)
        {
            tradeUI.gameObject.SetActive(true);
            tradeUI.SetUpTrade(item);
        }

        private void OnBagCloseEvent(SlotType slotType, InventoryBag_SO bagData)
        {
            shop.SetActive(false);
            itemToolTip.gameObject.SetActive(false);
            foreach (var slot in baseBagSlots)
            {
                Destroy(slot.gameObject);
            }
            
           baseBagSlots.Clear();
            

        }
        public void OnBagOpenEvent(SlotType slotType, InventoryBag_SO bagData)
        {
           
            
            
            shop.SetActive(true);
            baseBagSlots = new List<SlotUI>();

            for (int i = 0; i < bagData.itemList.Count; i++)
            {
                
                var slot = Instantiate(shopSlotPrefab, shop.transform.GetChild(0)).GetComponent<SlotUI>();
                slot.slotIndex = i;
                baseBagSlots.Add(slot);
                
            }
            
           
            LayoutRebuilder.ForceRebuildLayoutImmediate(shop.GetComponent<RectTransform>());

            OnUpdateInventoryUI(InventoryLocation.Shop,bagData.itemList);
        }
        private void OnUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
        {
            switch (location)
            {
                case InventoryLocation.Player:
                    for (int i = 0; i < playerSlots.Length; i++)
                    {
                        if (list[i].itemAmount > 0)
                        {
                            var item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                            playerSlots[i].UpdateSlot(item, list[i].itemAmount);
                        }
                        else
                        {
                            playerSlots[i].UpdateEmptySlot();
                        }
                    }
                    break;

                case InventoryLocation.Shop:
                    for (int i = 0; i < baseBagSlots.Count; i++)
                    {
                        if (list[i].itemAmount > 0)
                        {
                            var item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                            baseBagSlots[i].UpdateSlot(item, list[i].itemAmount);
                           
                        }
                        else
                        {
                           
                            baseBagSlots[i].UpdateEmptySlot();
                        }
                    }
                    break;
                  
            }
        }

        public void OpenBagUI()
        {
            bagOpened = !bagOpened;
            bagUI.SetActive(bagOpened);
        }
        public void UpdateSlotHightlight(int index)
        {
            foreach (var slot in playerSlots)
            {
                if (slot.isSelected && slot.slotIndex == index)
                {
                    slot.slotHighLight.gameObject.SetActive(true);
                }
                else
                {
                    slot.isSelected = false;
                    slot.slotHighLight.gameObject.SetActive(false);
                }
            }
        }

    }
}