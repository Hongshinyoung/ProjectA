using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : BaseCharacter
{
    public override float MoveSpeed => 5f;

    public override float DashSpeed => 10f;

    public override void UseSkill()
    {
        Debug.Log("경찰 스킬 실행");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsCollision(collisionLayer, collision.gameObject))
        {
            Debug.Log("도둑 잡기 성공");
        }
    }
}
