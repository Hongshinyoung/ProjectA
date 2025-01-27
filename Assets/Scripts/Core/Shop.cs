using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<Item> shopItems = new List<Item>();
    private UserData userData;
    
    public Item BuyItem(Item item)
    {
        if (userData.Gold >= item.ItemPrice)
        {
            userData.Gold -= item.ItemPrice;
            return item;
        }
        return null;
    }

    public Item SellItem(Item item)
    {
        if (item != null)
        {
            userData.Gold += item.ItemPrice;
            return item;
        }
        return null;
    }
}
