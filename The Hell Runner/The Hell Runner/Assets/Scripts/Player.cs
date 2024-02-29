using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D m_rb;

    [SerializeField] private bool m_isGrounded;
    [SerializeField] private bool m_isSliding;
    [SerializeField] private int m_speed;
    [SerializeField] private int m_jumpForce;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        m_rb.velocity = new Vector2(h * m_speed, m_rb.velocity.y);
    }

    private void Jump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f);

        if (!hit)
        {
            return;
        }

        m_rb.velocity = new Vector2(m_rb.velocity.x, m_jumpForce);
    }

    private void WallJump()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
