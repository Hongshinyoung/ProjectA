using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory> // 싱글톤으로 구현
{
    public List<Item> items = new List<Item>();
    [HideInInspector] public UIInventory uiInventory;
    [HideInInspector] public Shop shop;
    protected override void Awake()
    {
        base.Awake();   
    }
    
    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }
    
    public void UseItem(Item item) // 소비형 아이템은 사용 후 삭제, 장착형 아이템은 유지
    {
        if (items.Contains(item))
        {
            item.Use();
            if (item.Type == ItemType.Consumable)
            {
                RemoveItem(item);
            }
            else
            {
                Debug.Log("인벤토리에 아이템이 없음");
            }
        }
    }
}
