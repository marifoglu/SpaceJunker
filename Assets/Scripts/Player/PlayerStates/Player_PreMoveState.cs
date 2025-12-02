using UnityEngine;

public class Player_PreMoveState : EntityState
{

    private float preAnimationTimer;
    private readonly float preAnimationDuration = 0.5f; // INCREASE THIS! Make it 0.5 seconds

    public Player_PreMoveState(Player player, StateMachine stateMachine, string stateName)
        : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, 0);
        preAnimationTimer = preAnimationDuration; // RESET TIMER HERE
        Debug.Log($"Entered MOVEPRE state | Timer: {preAnimationTimer}");
    }

    public override void Update()
    {
        base.Update();

        // Debug the timer
        Debug.Log($"MovePre Timer: {preAnimationTimer}");

        preAnimationTimer -= Time.deltaTime;

        // ADD THIS DEBUG TO SEE WHAT'S HAPPENING
        if (preAnimationTimer <= 0)
        {
            Debug.Log($"MovePre Timer EXPIRED! Changing to Move state");
            stateMachine.ChangeState(player.moveState);
        }

        // Optional: Allow canceling back to idle if input stops
        if (player.moveInput == Vector2.zero)
        {
            Debug.Log($"MovePre INPUT STOPPED! Changing to Idle state");
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting MOVEPRE state");
    }
}