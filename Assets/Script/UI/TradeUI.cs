using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
namespace yhzs.Inventory
{
    public class TradeUI : MonoBehaviour
    {
        public Button submitButton;
        public Button cancelButton;
        private ItemDetails item;
        public TMP_InputField tradeAmount;


        private void Awake()
        {
            cancelButton.onClick.AddListener(CancelTrade);
            submitButton.onClick.AddListener(TradeItem);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetUpTrade(ItemDetails item)
        {
            this.item = item;
            tradeAmount.text = string.Empty;
        }
        private void TradeItem()
        {
            var amount = Convert.ToInt32(tradeAmount.text);
            InventoryManager.Instance.TradeItem(item, amount);
            this.gameObject.SetActive(false);
        }

        public void CancelTrade()
        {
            this.gameObject.SetActive(false);
        }

    }
}