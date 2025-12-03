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
        Debug.Log(">> IDLE STATE");
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput.magnitude > 0.1f)
        {
            Debug.Log($">> Input: {player.moveInput} -> SWITCHING TO MOVEPRE");
            stateMachine.ChangeState(player.movePreState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}