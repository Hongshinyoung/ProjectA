using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Button useButton;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    private int itemPrice;
    
    private Item curItem;

    public void SetSlot(Item item)
    {
        curItem = item;
        if (curItem != null)
        {
            itemName.text = curItem.name;
            itemIcon.sprite = curItem.ItemIcon;
            itemPrice = curItem.ItemPrice;
            useButton.onClick.AddListener(UseItem);
        }
    }

    private void UseItem()
    {
        if (curItem != null)
        {
            Inventory.Instance.UseItem(curItem);
            Inventory.Instance.uiInventory.RefreshUI();
            // 이벤트로 ui업데이트 해주기
        }
    }
    
    public void ClearSlot()
    {
        curItem= null;
        itemIcon = null;
        itemName.text = "";
        useButton.onClick.RemoveAllListeners();
    }
}
