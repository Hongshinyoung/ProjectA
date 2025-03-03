using System;
using UnityEngine;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        groundData = stateMachine.Player.PlayerData.GroundData;
    }
    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    protected virtual void AddInputActionsCallbacks()
    {
        
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        
    }

    public virtual void Update()
    {
        
    }
    
    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.animator.SetBool(animationHash, false);
    }

}
