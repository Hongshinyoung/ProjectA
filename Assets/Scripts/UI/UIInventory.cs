using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : UIBase
{
    [SerializeField] private Transform uiTransformParent;
    [SerializeField] private GameObject inventoryItemPrefab;
    
    private Inventory inventory;
    
    private void Awake()
    {
        inventory = Inventory.Instance;
        // 인벤토리 슬롯 동적생성
            for (int i = 0; i < inventory.items.Count; i++)
        {
            GameObject itemSlot = ResourceManager.Instance.LoadAsset<GameObject>("InventorySlot", eAssetType.UI);
            Instantiate(itemSlot, uiTransformParent);
        }
    }
}
