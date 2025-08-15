using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace yhzs.Inventory { 
    [RequireComponent(typeof(SlotUI))]
public class ShowItemToolTip : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
        private SlotUI slotUI;
        private InventoryUI inventoryUI;

        private void Awake()
        {
            slotUI = GetComponent<SlotUI>();
        }
        private void Start()
        {
            inventoryUI = GetComponentInParent<InventoryUI>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (slotUI.itemAmount != 0)
            {
                inventoryUI.itemToolTip.gameObject.SetActive(true);
                inventoryUI.itemToolTip.SetUpToolTip(slotUI.itemDetails, slotUI.slotType);
            }
            else
            {
                inventoryUI.itemToolTip.gameObject.SetActive(false);
            }
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            inventoryUI.itemToolTip.gameObject.SetActive(false);
        }
        // Start is called before the first frame update

    }
}