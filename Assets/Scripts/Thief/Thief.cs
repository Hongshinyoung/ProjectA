using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Thief : BaseCharacter
{
    public override float MoveSpeed => 5.2f;
    public override float DashSpeed => 9f;
    private CharacterController controller;
    private bool isInprison = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }


    public override void UseSkill()
    {
        // 첫 번째 스킬, 불: 주변에 불을 확산시키고, 경찰들이 제한시간내에 불을 끄지 못하면 패배
        GameObject fireObj = ResourceManager.Instance.LoadAsset<GameObject>("Fire",eAssetType.Prefab);
        if (fireObj != null)
        {
            GameObject obj = Instantiate(fireObj, transform.position + new Vector3(0,0,1), Quaternion.identity);
        }
        Debug.Log(" 도둑 스킬 ");
    }

    [PunRPC]
    public void SyncPrisonPosition(Vector3 position) //위치 동기화
    {
        Debug.Log("동기화 실행감옥");
        transform.position = position;
        isInprison = true;
        GameManager.Instance.CheakGameEnd();
    }

    public bool IsPrison()
    {
        return isInprison;
    }
}
