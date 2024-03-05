using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D m_rb;

    [SerializeField] private bool m_isSliding;
    [SerializeField] private int m_speed;
    [SerializeField] private int m_jumpForce;

    private bool m_canWallJump;
    private Vector2 m_pushDir;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
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
    }

    private void FixedUpdate()
    {
        if (!m_canWallJump)
        {
            float h = Input.GetAxis("Horizontal");

            m_rb.velocity = new Vector2(h * m_speed, m_rb.velocity.y);
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
        if (collision.gameObject.tag == "Tile" && !IsGrounded())
        {
            RaycastHit2D leftRay = Physics2D.Raycast(transform.position, Vector2.left, 1.1f);
            RaycastHit2D rightRay = Physics2D.Raycast(transform.position, Vector2.right, 1.1f);

            m_canWallJump = true;

            if (!leftRay && !rightRay)
            {
                return;
            }

            else if (leftRay && !rightRay)
            {
                m_pushDir = new Vector2(2f, 1f) * m_jumpForce;
            }

            else if (!leftRay && rightRay)
            {
                m_pushDir = new Vector2(-2f, 1f) * m_jumpForce;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Tile"))
        {
            m_canWallJump = false;
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
