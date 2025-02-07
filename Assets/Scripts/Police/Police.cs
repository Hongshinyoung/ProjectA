using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : BaseCharacter
{
    [SerializeField] private LayerMask detectLayer;
    [SerializeField] private LayerMask elevatorLayer;
    [SerializeField] private Transform prisonPosition;
    private CharacterController controller;
    public override float MoveSpeed => 5f;

    public override float DashSpeed => 10f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        var prison = GameObject.Find("Prison(Clone)");
        prisonPosition = prison.transform;
    }

    public override void UseSkill()
    {
        Debug.Log("스킬");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(!photonView.IsMine) return;
        if (IsCollision(detectLayer, hit.collider.gameObject))
        {
            Thief thiefController = hit.collider.GetComponent<Thief>();
            if (thiefController != null)
            {
                PhotonView theifView = thiefController.GetComponent<PhotonView>();
                if (theifView != null)
                {
                    theifView.RPC("SyncPrisonPosition", RpcTarget.All, prisonPosition.position + new Vector3(0,2,0));
                    Debug.Log("도둑 감옥으로 이동");
                }
            }
        }
        if ((1 << hit.collider.gameObject.layer & elevatorLayer) != 0)
        {
            Elevator elevator = hit.collider.gameObject.GetComponent<Elevator>();
            if (elevator != null)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    elevator.OnDown();
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    elevator.OnLift();
                }
            }
        }
    }
}
