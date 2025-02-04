using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : BaseCharacter
{
    public override float MoveSpeed => 5.2f;

    public override float DashSpeed => 9f;

    public override void UseSkill()
    {
        Debug.Log(" 도둑 스킬 실행");
    }
}
