using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable,
    Equipment
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int ItemID;
    public string ItemName;
    public int ItemPrice;
    public Sprite ItemIcon;
    public ItemType Type;
    public bool isConsumable;
    public int MaxStack;
    public int HealAmount;
    public int AttackPower;
    public int DefensePower;

    public virtual void Use()
    {
        Debug.Log("아이템 사용");
    }
}