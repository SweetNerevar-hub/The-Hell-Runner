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

    private void Descend()
    {
        if (m_currentSpeed < m_maxSpeed)
        {
            m_currentSpeed += m_acceleration * Time.deltaTime;
        }

        m_truePosition -= m_currentSpeed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x, m_truePosition);
    }
}
