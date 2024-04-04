using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    Rigidbody2D m_rb;
    Animator m_animator;

    [SerializeField] private EventsManager m_events;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
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
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_rb.velocity.y < 0 && collision.CompareTag("Hazard"))
        {
            m_animator.SetBool("IsDead", true);
            m_events.Event_OnPlayerDeath();
        }

        else if (collision.CompareTag("Soul"))
        {
            collision.GetComponent<Soul>().PlayCollectedAnim();
        }
    }
}
