using UnityEngine;

public class StateMachine
{
    public IState CurrentState { get; private set; }

    public PlayerIdleState idleState;
    public WalkState walkState;
    public RunState runState;

    public StateMachine(PlayerController player)
    {
        //idleState = new PlayerIdleState(player);
        walkState = new WalkState(player);
        runState = new RunState(player);
    }

    public void Initialize(IState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void TransitionToState(IState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }

    public void Update()
    {
        if(CurrentState != null)
        {
            CurrentState.Update();
        }
    }
}
