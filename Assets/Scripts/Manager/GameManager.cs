using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    private UserData userData;
    private SpawnManager spawnManager;
    public SpawnManager SpawnManager => spawnManager;

    protected override void Awake()
    {
        base.Awake();
        userData = new UserData(10000);
    }

    public UserData GetPlayerStatus()
    {
        return userData;
    }

    public void EnterInGame(string selectCharacter)
    {
        SceneLoadManager.Instance.LoadScene("InGame",() => 
        {
            spawnManager = new GameObject("SpawnManager").AddComponent<SpawnManager>();
            spawnManager.Initialize(selectCharacter);
        });
    }
    
}
