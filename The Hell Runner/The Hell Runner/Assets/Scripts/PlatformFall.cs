using UnityEngine;

public class PlatformFall : MonoBehaviour
{
    [SerializeField] private float m_acceleration;
    [SerializeField] private float m_currentSpeed;
    [SerializeField] private float m_maxSpeed;

    private float m_truePosition;

    private void Start()
    {
        m_truePosition = transform.position.y;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Descend();
        }
    }

    // TODO:    Make a bool that is set to true when the player hits the platform
    //          this will hopefully stop the jittering since the platforms only moves when the player it colliding with it,
    //          and the player stops colliding when the platform moves down a little and before the player falls back into it
    //          thus, jitter.
    private void Descend()
    {
        if (m_currentSpeed < m_maxSpeed)
        {
            m_currentSpeed += m_acceleration * Time.deltaTime;
        }

        m_truePosition -= m_currentSpeed;
        transform.position = new Vector2(transform.position.x, m_truePosition);
    }
}
