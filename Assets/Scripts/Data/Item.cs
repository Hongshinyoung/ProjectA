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
    public bool Consumable;
    public int MaxStack;

    public virtual void Use()
    {
        Debug.Log("아이템 사용");
    }
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Consumable")]
public class Consumable : Item
{
    public int HealAmount;

    public override void Use()
    {
        base.Use();
    }
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public int AttackPower;
    public int DefencePower;

    public override void Use()
    {
        base.Use();
    }
}