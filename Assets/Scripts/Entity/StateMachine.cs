using UnityEngine;

public class StateMachine
{
    public EntityState currentState { get; private set; }

    public void Initialize(EntityState startingState)
    {
        if (startingState == null)
        {
            Debug.LogError("Cannot initialize StateMachine with null state!");
            return;
        }

        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(EntityState newState)
    {
        if (newState == null)
        {
            Debug.LogError("Cannot change to null state!");
            return;
        }

        currentState?.Exit(); // Use null conditional operator
        currentState = newState;
        currentState.Enter();
    }

    public void UpdateActiveState()
    {
        currentState?.Update(); // Use null conditional operator
    }
}