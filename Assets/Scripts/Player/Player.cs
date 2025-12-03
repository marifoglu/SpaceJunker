using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputSet input;
    private StateMachine stateMachine;

    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_PreMoveState movePreState { get; private set; }

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
        moveState = new Player_MoveState(this, stateMachine, "Move");
        movePreState = new Player_PreMoveState(this, stateMachine, "MovePre");
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
    }

    private void UpdateAnimatorParameters()
    {
        if (moveInput != Vector2.zero)
        {
            Vector2 normalizedInput = moveInput.normalized;
            anim.SetFloat("moveX", normalizedInput.x);
            anim.SetFloat("moveY", normalizedInput.y);
            lastDirection = normalizedInput;
        }
       

            anim.SetFloat("lastDirX", lastDirection.x);
        anim.SetFloat("lastDirY", lastDirection.y);
        anim.SetFloat("speed", moveInput.magnitude);
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
    }
}