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
