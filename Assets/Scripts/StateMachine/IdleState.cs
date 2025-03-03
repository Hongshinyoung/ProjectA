using UnityEngine;

public class IdleState : IState
{
    private PlayerController player;

    public IdleState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("IdleState Enter");
    }

    public void Exit()
    {
        Debug.Log("IdleState Exit");
    }

    public void Update()
    {
        
    }
}
