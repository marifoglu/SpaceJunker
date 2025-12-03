using UnityEngine;

public class Player_MoveState : EntityState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string stateName)
        : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log(">> MOVE STATE - Player is moving");
    }

    public override void Update()
    {
        base.Update();

        // Check for input stop FIRST
        if (player.moveInput.magnitude < 0.1f)
        {
            Debug.Log(">> Movement stopped -> IDLE");
            stateMachine.ChangeState(player.idleState);

            return;
        }

        // Apply movement
        Vector2 movement = player.moveInput.normalized * player.moveSpeed;
        player.SetVelocity(movement.x, movement.y);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, 0);
    }
}