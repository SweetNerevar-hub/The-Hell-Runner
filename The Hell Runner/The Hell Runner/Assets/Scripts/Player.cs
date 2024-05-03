using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private Animator m_animator;
    private SpriteRenderer m_spriteRenderer;
    private PlayerSounds m_sounds;

    [SerializeField] private EventsManager m_events;
    [SerializeField] private SceneHandler m_sceneHandler;
    [SerializeField] private Camera m_camera;
    [SerializeField] private Hat hat;

    [SerializeField] private bool m_isSliding;
    [SerializeField] private int m_speed;
    [SerializeField] private float m_acceleration;
    [SerializeField] private int m_jumpForce;

    [SerializeField] private float m_maxCoyoteTime;
    [SerializeField] private float m_currentCoyoteTime;

    private bool m_canWallJump;
    private Vector2 m_pushDir;
    private const float m_rayLengthDown = 0.75f;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_sounds = GetComponent<PlayerSounds>();
    }

    private void Start()
    {
        m_events.onPlayerDeath += OnDeath;

        hat.SetCosmetic();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded() || m_currentCoyoteTime > 0f)
            {
                Jump();
                m_animator.SetBool("IsJumping", true);
            }

            else if (m_canWallJump)
            {
                WallJump(m_pushDir);
            }
        }

        CoyoteTimeRay();
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
        Vector2 topRayPos = new Vector2(transform.position.x, transform.position.y + 0.5f);
        Vector2 bottomRayPos = new Vector2(transform.position.x, transform.position.y - 0.5f);
        float rayLength = 0.35f;

        RaycastHit2D topRay = Physics2D.Raycast(topRayPos, Vector2.right, rayLength);
        RaycastHit2D bottomRay = Physics2D.Raycast(bottomRayPos, Vector2.right, rayLength);

        m_canWallJump = true;

        if (!topRay && !bottomRay || IsGrounded())
        {
            m_canWallJump = false;
            m_animator.SetBool("IsWallSliding", false);
        }

        else if (topRay.collider.tag == "Tile" || bottomRay.collider.tag == "Tile")
        {
            //m_currentCoyoteTime = m_maxCoyoteTime;
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
        m_sounds.PlayJumpSound();
    }

    private void WallJump(Vector2 pushDir)
    {
        m_rb.velocity = pushDir;
        m_sounds.PlayJumpSound();
    }

    private void OnDeath()
    {
        m_sounds.PlayDeathSound();
        m_rb.gravityScale = 0;
        m_rb.velocity = Vector2.zero;
        enabled = false;
    }

    private void CoyoteTimeRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, m_rayLengthDown);

        if (!hit && m_currentCoyoteTime > 0f)
        {
            m_currentCoyoteTime -= Time.deltaTime;
        }
        
        else if (hit)
        {
            if (hit.collider.tag == "Tile")
            {
                m_currentCoyoteTime = m_maxCoyoteTime;
            }
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, m_rayLengthDown);

        if (!hit)
            return false;

        return true;
    }

    private void OnDisable()
    {
        m_events.onPlayerDeath -= OnDeath;
    }
}
