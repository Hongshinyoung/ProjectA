using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;

    private void Awake()
    {
        if (spawnPos == null)
        {
            spawnPos = new GameObject("SpawnPos").transform;
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

        if (obj != null)
            Debug.Log($"{selectPrefabName} 생성 완료");
        else
            Debug.LogError($"{selectPrefabName} 프리팹을 찾을 수 없습니다.");

    }
}
