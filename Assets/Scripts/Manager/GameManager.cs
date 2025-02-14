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

    public void CheakGameEnd()
    {
        int thievesInPrison = 0;

        foreach (GameObject player in spawnManager.spawnPlayers)
        {
            Thief thief = player.GetComponent<Thief>();
            if (thief != null && thief.IsPrison())
            {
                thievesInPrison++;
            }
        }

        if (thievesInPrison >= 4)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("게임 오버 경찰 승");
    }
    
}
