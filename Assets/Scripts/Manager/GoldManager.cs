using System;
using UnityEngine;

public class GoldManager : Singleton<GoldManager>
{
    // TODO: 골드 벌기, 줄어들기
    private int gold;
    public event Action<int> OnGoldChanged;
    public int Gold
    {
        get
        {
            return Mathf.Max(0, gold);
        }
        set
        {
            gold = value;
            OnGoldChanged?.Invoke(gold);
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;
    }

    public void SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
        }
    }
    
}
