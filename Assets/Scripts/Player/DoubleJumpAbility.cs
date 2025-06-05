using UnityEngine;

public class DoubleJumpAbility : MonoBehaviour
{
    public int extraJumps = 1;

    private int jumpsLeft;
    private PlayerController player;
    private Rigidbody2D rb;

    void Awake()
    {
        player = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        jumpsLeft = extraJumps;
        player.inputActions.Player.Jump.performed += _ => TryDoubleJump();
    }

    void OnDisable()
    {
        player.inputActions.Player.Jump.performed -= _ => TryDoubleJump();
    }

    void Update()
    {
        if (player.IsGrounded)
            jumpsLeft = extraJumps;
    }

    void TryDoubleJump()
    {
        if (!player.IsGrounded && jumpsLeft > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, player.jumpForce);
            jumpsLeft--;
        }
    }
}
