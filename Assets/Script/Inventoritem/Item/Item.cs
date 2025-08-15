using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yhzs.Inventory { 
public class Item : MonoBehaviour
{
        public int itemID;
        private SpriteRenderer sr;
        public ItemDetails itemDetails;





        private void Awake()
        {
            sr = GetComponentInChildren<SpriteRenderer>();
        }

        // Start is called before the first frame update
        void Start()
    {
            if (itemID != 0)
            {
                Init(itemID);
            }
        }

    // Update is called once per frame
    void Update()
    {
        
    }

        public void Init(int ID)
        {
            itemID = ID;
            itemDetails = InventoryManager.Instance.GetItemDetails(itemID);
            if (itemDetails != null)
            {
                sr.sprite = itemDetails.itemOnWorldSprite != null ? itemDetails.itemOnWorldSprite : itemDetails.itemIcon;
            }
        }


    }
}
