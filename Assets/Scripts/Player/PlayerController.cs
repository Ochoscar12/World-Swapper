using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Cooldown Settings")]
    public float jumpCooldown = 0f;
    [Header("World References")]
    public GameObject worldA;
    public GameObject worldB;


    private Rigidbody2D rb;
    public Vector2 moveInput;
    private bool jumpPressed;
    private float lastJumpTime = -Mathf.Infinity;

    [HideInInspector] public PlayerInputActions inputActions;
    [HideInInspector] public bool IsGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += _ => moveInput = Vector2.zero;
        inputActions.Player.Jump.started += _ => jumpPressed = true;
        inputActions.Player.RestartLevel.performed += _ => RestartScene();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (jumpPressed && IsGrounded && Time.time >= lastJumpTime + jumpCooldown)
        {
            Jump();
            jumpPressed = false;
            lastJumpTime = Time.time;
        }

        if (jumpPressed && Time.time < lastJumpTime + jumpCooldown)
        {
            jumpPressed = false;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    public void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        if (groundCheck != null)
            Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
    }
        public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

