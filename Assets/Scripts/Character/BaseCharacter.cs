using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    [SerializeField] protected LayerMask collisionLayer;
    public abstract float MoveSpeed { get; }
    public abstract float DashSpeed { get; }

    public abstract void UseSkill();
    protected bool IsCollision(LayerMask touchLayer, GameObject obj)
    {
        return ((1 << obj.layer) & touchLayer) != 0;
    }
}
