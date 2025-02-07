using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : BaseCharacter
{
    [SerializeField] private LayerMask detectLayer;
    [SerializeField] private LayerMask elevatorLayer;
    private CharacterController controller;
    public override float MoveSpeed => 5f;

    public override float DashSpeed => 10f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
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
                thiefController.GoToPrison();
            }
            Debug.Log("도둑 잡음");
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
