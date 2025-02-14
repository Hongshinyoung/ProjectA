using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;
    public List<GameObject> spawnPlayers = new List<GameObject>();

    private void Awake()
    {
        if (spawnPos == null)
        {
            spawnPos = new GameObject("SpawnPos").transform; // 기본 0,0,0으로 세팅
            spawnPos.transform.position = new Vector3(6,3,0);
        }
    }

    public void Initialize(string selectPrefabName)
    {
        if (string.IsNullOrEmpty(selectPrefabName))
        {
            Debug.LogError("선택된 프리팹 이름이 없습니다.");
            return;
        }
        
        var obj = PhotonNetwork.Instantiate(selectPrefabName, spawnPos.position, Quaternion.identity);
        spawnPlayers.Add(obj);

        if (obj != null)
            Debug.Log($"{selectPrefabName} 생성 완료");
        else
            Debug.LogError($"{selectPrefabName} 프리팹을 찾을 수 없습니다.");

    }
}
