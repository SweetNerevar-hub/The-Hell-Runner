using UnityEngine;

public class PlatformFall : MonoBehaviour
{
    float m_acceleration = 0.04f;
    private float m_currentSpeed;
    private float m_maxSpeed = 1f;
    private float m_truePosition;
    private bool m_fallFlag;

    private void Start()
    {
        m_truePosition = transform.position.y;
    }

    private void Update()
    {
        if (m_fallFlag)
        {
            Descend();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            m_fallFlag = true;            
        }
    }

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
