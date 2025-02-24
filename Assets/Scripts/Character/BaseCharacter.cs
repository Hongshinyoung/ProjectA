using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviourPun
{
    [SerializeField] protected LayerMask collisionLayer;
    public abstract float MoveSpeed { get; }
    public abstract float DashSpeed { get; }

    public abstract void FirstSkill();
    public abstract void SecondSkill();
    protected bool IsCollision(LayerMask touchLayer, GameObject obj)
    {
        return ((1 << obj.layer) & touchLayer) != 0;
    }
}
