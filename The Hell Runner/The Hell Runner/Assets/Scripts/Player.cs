using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private Animator m_animator;
    private SpriteRenderer m_spriteRenderer;

    [SerializeField] private EventsManager m_events;
    [SerializeField] private Camera m_camera;

    [SerializeField] private bool m_isSliding;
    [SerializeField] private int m_speed;
    [SerializeField] private float m_acceleration;
    [SerializeField] private int m_jumpForce;

    private bool m_canWallJump;
    private Vector2 m_pushDir;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        m_events.onPlayerDeath += DisablePlayerMovement;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // TODO: Add coyote time to make jumping feel better
            if (IsGrounded())
            {
                Jump();
                m_animator.SetBool("IsJumping", true);
            }

            else if (m_canWallJump)
            {
                WallJump(m_pushDir);
            }
        }

        WallJumpCheck();
        PlayerInBoundsCheck();
        SetVerticalAnimState();
    }

    private void FixedUpdate()
    {
        if (!m_canWallJump)
        {
            float h = Input.GetAxis("Horizontal");

            m_rb.velocity = new Vector2(h * m_speed, m_rb.velocity.y);
            SetRunningAnimState(h);
        }
    }

    private void SetRunningAnimState(float h)
    {
        if (h > 0)
        {
            m_animator.SetBool("IsRunning", true);
            m_spriteRenderer.flipX = false;
            return;
        }

        if (h < 0)
        {
            m_animator.SetBool("IsRunning", true);
            m_spriteRenderer.flipX = true;
            return;
        }

        m_animator.SetBool("IsRunning", false);
    }

    private void SetVerticalAnimState()
    {
        switch (m_rb.velocity.y)
        {
            case > 0:
                m_animator.SetBool("IsJumping", true);
                m_animator.SetBool("IsFalling", false);
                break;

            case < 0f:
                if (!m_animator.GetBool("IsWallSliding"))
                {
                    m_animator.SetBool("IsFalling", true);
                }
                
                m_animator.SetBool("IsJumping", false);
                break;

            default:
                m_animator.SetBool("IsJumping", false);
                m_animator.SetBool("IsFalling", false);
                break;
        }
    }

    private void WallJumpCheck()
    {
        // TODO: Add a second ray, one will point from the players neck, the other from their knees
        // This is to make wall jumps feel more responsive by increasing the range in which the player can wall jump
        RaycastHit2D rightRay = Physics2D.Raycast(transform.position, Vector2.right, 0.35f);

        m_canWallJump = true;

        if (!rightRay || IsGrounded())
        {
            m_canWallJump = false;
            m_animator.SetBool("IsWallSliding", false);
        }

        else if (rightRay.collider.tag == "Tile")
        {
            m_pushDir = new Vector2(-3, 1.35f) * m_jumpForce;

            if (!IsGrounded() && m_rb.velocity.y < 0)
            {
                m_animator.SetBool("IsWallSliding", true);
            }
        }
    }

    private void PlayerInBoundsCheck()
    {
        Vector2 playerPosInCamera = m_camera.WorldToViewportPoint(transform.position);

        if (playerPosInCamera.x < 0.025f)
        {
            m_animator.SetBool("IsDead", true);
            m_events.Event_OnPlayerDeath();
        }
    }

    private void Jump()
    {
        m_rb.velocity = new Vector2(m_rb.velocity.x, m_jumpForce);
    }

    private void WallJump(Vector2 pushDir)
    {
        m_rb.velocity = pushDir;
    }

    private void DisablePlayerMovement()
    {
        m_rb.gravityScale = 0;
        m_rb.velocity = Vector2.zero;
        enabled = false;
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.35f);

        if (!hit) return false;

        return true;
    }

    private void OnDisable()
    {
        m_events.onPlayerDeath -= DisablePlayerMovement;
    }
}
