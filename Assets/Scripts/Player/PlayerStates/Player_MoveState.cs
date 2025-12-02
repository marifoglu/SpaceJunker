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
        Debug.Log("Entered MOVE state");
    }

    public override void Update()
    {
        base.Update();

        // Apply movement
        Vector2 movement = player.moveInput.normalized * player.moveSpeed;
        player.SetVelocity(movement.x, movement.y);

        // Return to idle if no input
        if (player.moveInput == Vector2.zero)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting MOVE state");
    }
}