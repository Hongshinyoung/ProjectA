using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private UserData userData;

    protected override void Awake()
    {
        base.Awake();
        userData = new UserData(10000);
    }

    public UserData GetPlayerStatus()
    {
        return userData;
    }
    
}
