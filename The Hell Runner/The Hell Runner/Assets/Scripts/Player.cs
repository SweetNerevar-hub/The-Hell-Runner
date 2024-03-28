using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private Animator m_animator;
    private SpriteRenderer m_spriteRenderer;

    [SerializeField] private bool m_isSliding;
    [SerializeField] private int m_speed;
    [SerializeField] private int m_jumpForce;

    private bool m_canWallJump;
    private Vector2 m_pushDir;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
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

        SetVerticalAnimState();
        WallJumpCheck();
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
                m_animator.SetBool("IsFalling", true);
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
        RaycastHit2D rightRay = Physics2D.Raycast(transform.position, Vector2.right, 0.25f);

        m_canWallJump = true;

        if (!rightRay || IsGrounded())
        {
            m_canWallJump = false;
            m_animator.SetBool("IsWallSliding", false);
            return;
        }

        else if (rightRay.collider.tag == "Tile")
        {
            m_pushDir = new Vector2(-2f, 1f) * m_jumpForce;

            if (!IsGrounded() && m_rb.velocity.y < 0)
            {
                m_animator.SetBool("IsWallSliding", true);
            }

            return;
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

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f);

        if (!hit) return false;

        m_canWallJump = false;
        return true;
    }
}
