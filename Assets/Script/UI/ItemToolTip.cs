using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ItemToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image itemImage;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpToolTip(ItemDetails itemDetails,SlotType slotType)
    {
        nameText.text = itemDetails.name;
        descriptionText.text = itemDetails.itemDesciption;
        itemImage.sprite = itemDetails.itemIcon;
    }


}
