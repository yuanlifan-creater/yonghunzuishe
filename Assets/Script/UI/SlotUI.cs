using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace yhzs.Inventory {
    public class SlotUI : MonoBehaviour, IPointerClickHandler
    {
        [Header("组件获取")]
        [SerializeField] private Image slotImage;
        [SerializeField] private TextMeshProUGUI amountText;
        public  Image slotHighLight;
        [SerializeField] private Button button;

        [Header("格子类型")]
        public SlotType slotType;
        public bool isSelected;

        public ItemDetails itemDetails;
        public int itemAmount;
        public int slotIndex;
        //private InventoryUI inventoryUI;
        // Start is called before the first frame update
        void Start()
        {
            isSelected = false;
            if (itemDetails.itemID == 0)
            {
                UpdateEmptySlot();
            }
            //inventoryUI = GetComponentInParent<InventoryUI>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void UpdateSlot(ItemDetails item, int amount)
        {
            itemDetails = item;
            slotImage.sprite = item.itemIcon;
            itemAmount = amount;
            amountText.text = amount.ToString();
            slotImage.enabled = true;
            button.interactable = true;
        }

        public void UpdateEmptySlot()
        {
            if (isSelected)
            {
                isSelected = false;
            }
            slotImage.enabled = false;
            amountText.text = string.Empty;
            button.interactable = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (itemAmount == 0) return;

            if (slotType == SlotType.playerBag)
            {
                isSelected = !isSelected;
                //inventoryUI.UpdateSlotHightlight(slotIndex);
            } 
            else if (slotType == SlotType.NpcBag)
            {
                EventHandler.CallShowTradeUI(itemDetails);
            }


            
        }
    }
}