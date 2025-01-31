using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //[SerializeField] private Transform spawnTransform;  

    public void Initialize(string selectPrefabName)
    {
        if(selectPrefabName != null) Debug.Log(selectPrefabName);
        //spawnTransform.position = new Vector3(0, 0, 0);
        GameObject obj = ResourceManager.Instance.LoadAsset<GameObject>(selectPrefabName, eAssetType.Prefab);
        if(obj != null) Debug.Log($"{selectPrefabName} 생성완료");
        if(obj == null) Debug.Log($"{selectPrefabName} 이 없음");
        Instantiate(obj);
    }
}
