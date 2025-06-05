
using UnityEngine;

public class WallJumpAbility : MonoBehaviour
{
    public float wallJumpForce = 8f;
    public float wallJumpXForce = 8f;
    public bool IsTouchingWalk;
    public float wallCheckRadius = 0.2f;

    private PlayerController player;
    private Rigidbody2D rb;

    void Awake()
    {
        player = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        player.inputActions.Player.Jump.started += _ => TryWallJump();
    }

    void OnDisable()
    {
        player.inputActions.Player.Jump.canceled -= _ => TryWallJump();
    }

    void TryWallJump()
    {
        Debug.Log("Intentando saltar desde pared");
        if (!player.IsGrounded && IsTouchingWalk)
        {
            var direction = -player.moveInput.x;

            rb.linearVelocity = new Vector2(direction * wallJumpXForce, wallJumpForce);
        }
    }
    public void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Wall")
        {
            IsTouchingWalk = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Wall")
        {
            IsTouchingWalk = false;
        }
    }

}

