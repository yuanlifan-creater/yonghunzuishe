using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yhzs.Inventory
{
    public class ItemPickUp : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Item item = collision.GetComponent<Item>();

            if (item != null)
            {
                InventoryManager.Instance.AddItem(item,true);
            }
        }

    }
}
