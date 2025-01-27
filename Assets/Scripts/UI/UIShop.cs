using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShop : UIBase
{
    [SerializeField] private Transform slotTrasformParent;
    private Shop shop;
    private void Awake()
    {
        shop = Inventory.Instance.shop;
        RefreshUI();
    }

    private void RefreshUI()
    {
        foreach (Transform child in slotTrasformParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in shop.shopItems)
        {
            GameObject shopSlot = ResourceManager.Instance.LoadAsset<GameObject>("ShopSlot", eAssetType.UI);
            Instantiate(shopSlot, slotTrasformParent);
        }
    }
}
