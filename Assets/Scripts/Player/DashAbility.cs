using UnityEngine;

public class DashAbility : MonoBehaviour
{
    public float dashForce = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 2f;

    private PlayerController player;
    private Rigidbody2D rb;
    private bool isDashing;
    private float dashTimeLeft;
    private float lastDashTime = -Mathf.Infinity;

    void Awake()
    {
        player = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        player.inputActions.Player.Dash.performed += _ => TryDash();
    }

    void OnDisable()
    {
        player.inputActions.Player.Dash.performed -= _ => TryDash();
    }

    void Update()
    {
        if (isDashing)
        {
            dashTimeLeft -= Time.deltaTime;
            if (dashTimeLeft <= 0)
            {
                isDashing = false;
            }
        }
        player.animator.SetBool("dasheando", isDashing);
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.linearVelocity = new Vector2(player.inputActions.Player.Move.ReadValue<Vector2>().x * dashForce, 0f);
        }
    }

    void TryDash()
    {
        if (Time.time >= lastDashTime + dashCooldown && !isDashing)
        {
            StartDash();
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        lastDashTime = Time.time;
    }
}

