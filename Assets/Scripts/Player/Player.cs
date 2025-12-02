using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputSet input;
    private StateMachine stateMachine;

    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }  // Added this
    public Player_PreMoveState movePreState { get; private set; }  // Added this for pre-animation

    public Vector2 moveInput { get; private set; }
    public Vector2 lastDirection { get; private set; } = Vector2.down;

    [Header("Movement Stats")]
    [SerializeField] public float moveSpeed = 1f;

    private void Awake()
    {
        stateMachine = new StateMachine();
        input = new PlayerInputSet();

        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        idleState = new Player_IdleState(this, stateMachine, "Idle");
        moveState = new Player_MoveState(this, stateMachine, "Move");  // Initialize move state
        movePreState = new Player_PreMoveState(this, stateMachine, "MovePre");  // Initialize pre-animation state

    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.UpdateActiveState();
        UpdateAnimatorParameters();

        // ADD THIS FOR DEBUGGING:
        Debug.Log($"Current Animator State: {anim.GetCurrentAnimatorStateInfo(0).IsName("???")}");
        Debug.Log($"Is in Idle: {anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")}");
        Debug.Log($"Is in MovePre: {anim.GetCurrentAnimatorStateInfo(0).IsName("MovePre")}");
        Debug.Log($"Is in Move: {anim.GetCurrentAnimatorStateInfo(0).IsName("Move")}");
        Debug.Log($"Animator State Hash: {anim.GetCurrentAnimatorStateInfo(0).fullPathHash}");
        Debug.Log($"Normalized Time: {anim.GetCurrentAnimatorStateInfo(0).normalizedTime}");
    }

    private void UpdateAnimatorParameters()
    {
        // Feed direction to blend trees
        if (moveInput != Vector2.zero)
        {
            // Normalize for diagonal movement
            Vector2 normalizedInput = moveInput.normalized;
            anim.SetFloat("moveX", normalizedInput.x);
            anim.SetFloat("moveY", normalizedInput.y);

            // Store last direction for idle facing
            lastDirection = normalizedInput;
        }

        // Always update lastDir for idle blend tree
        anim.SetFloat("lastDirX", lastDirection.x);
        anim.SetFloat("lastDirY", lastDirection.y);
        anim.SetFloat("speed", moveInput.magnitude);
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
    }
}