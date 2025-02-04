using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private string selectPrefabName;
    private UserData userData;
    private SpawnManager spawnManager;
    private NetworkManager networkManager;
    public SpawnManager SpawnManager => spawnManager;
    public NetworkManager NetworkManager => networkManager;
    public string SelectPrefabName => selectPrefabName;

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
            if (spawnManager == null)
            {
                spawnManager = new GameObject("SpawnManager").AddComponent<SpawnManager>();
            }
            selectPrefabName = selectCharacter;
            //spawnManager.Initialize(selectCharacter); // 여기서 실행하니 룸 생성 전에 플레이어 생성 요청해서 오류 발생.
        });
    }
    
}
