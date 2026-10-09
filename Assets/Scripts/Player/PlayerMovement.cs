using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private UserActions actions;
    private Rigidbody2D rigidBody;
    private Animator animator;

    private Vector2 moveDirection;

    [Header("Config")]
    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        actions = new UserActions();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ReadMovement();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ReadMovement()
    {
        moveDirection = actions.Movement.Move.ReadValue<Vector2>();;
        animator.SetBool("Moving", false);

        if (moveDirection == Vector2.zero)
            return;

        Debug.Log($"Move Direction: {moveDirection.x}, {moveDirection.y}");
        animator.SetFloat("MoveX", moveDirection.x);
        animator.SetFloat("MoveY", moveDirection.y);
        animator.SetBool("Moving", true);

    }

    private void Move()
    {
        if (rigidBody == null) 
            return;

        rigidBody.MovePosition(rigidBody.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));
    }

    private void OnEnable()
    {
        if (actions != null)
            actions.Enable();
    }

    private void OnDisable()
    {
        if (actions != null)
            actions.Disable();
    }
}
