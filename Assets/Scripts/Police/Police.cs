using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : BaseCharacter
{
    public override float MoveSpeed => 5f;

    public override float DashSpeed => 10f;

    public override void UseSkill()
    {
        Debug.Log("���� ��ų ����");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsCollision(collisionLayer, collision.gameObject))
        {
            Debug.Log("���� ��� ����");
        }
    }
}
