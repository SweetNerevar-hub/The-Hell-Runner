using System.Xml.Serialization;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private Animator m_animator;

    [SerializeField] private bool m_isSliding;
    [SerializeField] private int m_speed;
    [SerializeField] private int m_jumpForce;

    private bool m_canWallJump;
    private Vector2 m_pushDir;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                Jump();
            }

            else if (m_canWallJump)
            {
                WallJump(m_pushDir);
            }
        }

        Debug.DrawRay(transform.position, Vector2.right * 0.25f, Color.red);

        WallJumpCheck();
    }

    private void FixedUpdate()
    {
        if (!m_canWallJump)
        {
            float h = Input.GetAxis("Horizontal");

            m_rb.velocity = new Vector2(h * m_speed, m_rb.velocity.y);

            if (m_rb.velocity.x > 0)
            {
                m_animator.SetBool("IsRunning", true);
            }

            else
            {
                m_animator.SetBool("IsRunning", false);
            }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Tile")
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Tile")
        {
            transform.parent = null;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f);

        if (!hit)
        {
            transform.parent = null;
            return false;
        }

        m_canWallJump = false;
        transform.parent = hit.collider.transform;
        return true;
    }
}
