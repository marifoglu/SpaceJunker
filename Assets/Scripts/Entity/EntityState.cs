using UnityEngine;

public abstract class EntityState
{
    protected StateMachine stateMachine;
    protected Player player;
    protected string animBoolName;

    protected Animator anim;
    protected Rigidbody2D rb;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;

        anim = player.anim;
        rb = player.rb;
    }

    public virtual void Enter()
    {
        // CRITICAL: Turn ALL booleans OFF first
        anim.SetBool("Idle", false);
        anim.SetBool("Move", false);
        anim.SetBool("MovePre", false);

        // Then turn THIS one ON
        anim.SetBool(animBoolName, true);
        Debug.Log($"[ANIM] Set {animBoolName} = TRUE");
    }

    public virtual void Update()
    {
    }

    public virtual void Exit()
    {
        anim.SetBool(animBoolName, false);
        Debug.Log($"[ANIM] Set {animBoolName} = FALSE");
    }
}