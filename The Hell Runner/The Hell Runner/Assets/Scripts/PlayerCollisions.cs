using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    Rigidbody2D m_rb;
    Animator m_animator;
    CapsuleCollider2D m_capsuleCollider;

    [SerializeField] private EventsManager m_events;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_rb.velocity.y < 0 && collision.CompareTag("Hazard"))
        {
            PlayerDeath();
        }

        else if (collision.CompareTag("Enemy"))
        {
            PlayerDeath();
        }

        else if (collision.CompareTag("Soul"))
        {
            collision.GetComponent<Soul>().PlayCollectedAnim();
        }
    }

    private void PlayerDeath()
    {
        m_capsuleCollider.enabled = false;
        m_animator.SetBool("IsDead", true);
        m_events.Event_OnPlayerDeath();
    }
}
