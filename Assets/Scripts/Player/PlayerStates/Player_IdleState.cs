using UnityEngine;

public class Player_IdleState : EntityState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string stateName)
        : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, 0);
        Debug.Log("Entered IDLE state");
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput != Vector2.zero)
        {
            Debug.Log("Input detected, changing to MOVEPRE state");
            stateMachine.ChangeState(player.movePreState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting IDLE state");
    }
}