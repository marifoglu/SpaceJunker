using UnityEngine;

public class Player_PreMoveState : EntityState
{
    private float timer;
    private float duration = 0.01f;

    public Player_PreMoveState(Player player, StateMachine stateMachine, string stateName)
        : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, 0);
        timer = 0f;
        Debug.Log(">> MOVEPRE STATE - Starting wind-up animation");
    }

    public override void Update()
    {
        base.Update();

        // Cancel if input stops
        if (player.moveInput.magnitude < 0.1f)
        {
            Debug.Log(">> Input cancelled -> BACK TO IDLE");
            stateMachine.ChangeState(player.idleState);
            anim.SetFloat("moveX", 0);
            anim.SetFloat("moveY", 0);
            return;
        }

        timer += Time.deltaTime;

        // Transition to Move after duration
        if (timer >= duration)
        {
            Debug.Log($">> Wind-up complete ({timer:F2}s) -> MOVE STATE");
            stateMachine.ChangeState(player.moveState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}