using UnityEngine;

public class Player_PreMoveState : EntityState
{
    private float timer;
    private float duration = 0.3f; // Short duration - adjust this to match your animation length

    public Player_PreMoveState(Player player, StateMachine stateMachine, string stateName)
        : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, 0);
        timer = 0f; // Reset timer
        Debug.Log("ENTERED MOVEPRE - Timer Reset");
    }

    public override void Update()
    {
        base.Update();

        timer += Time.deltaTime;
        Debug.Log($"MovePre Timer: {timer:F2} / {duration:F2}");

        // If timer finished, go to Move state
        if (timer >= duration)
        {
            Debug.Log("Timer DONE! Going to MOVE state");
            stateMachine.ChangeState(player.moveState);
            return;
        }

        // If input stops, go back to idle
        if (player.moveInput == Vector2.zero)
        {
            Debug.Log("Input stopped! Going to IDLE");
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("EXITED MOVEPRE");
    }
}