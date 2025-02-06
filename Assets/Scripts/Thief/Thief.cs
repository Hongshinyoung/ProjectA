using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Thief : BaseCharacter
{
    public override float MoveSpeed => 5.2f;
    public override float DashSpeed => 9f;
    [SerializeField] private Transform prisonPosition;
    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        var prison = GameObject.Find("Prison(Clone)");
        prisonPosition = prison.transform;
    }

    public override void UseSkill()
    {
        Debug.Log(" 도둑 스킬 ");
    }

    public void GoToPrison()
    {
        if(!photonView.IsMine) return;
        photonView.RPC("SyncPrisonPosition", RpcTarget.All, prisonPosition.position);
    }

    [PunRPC]
    private void SyncPrisonPosition(Vector3 position) //위치 동기화
    {
        transform.position = position;
    }
}
