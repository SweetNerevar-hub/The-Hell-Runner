using UnityEngine;

public class PlatformMoveVertical : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private float m_distanceBetweenStart;
    [SerializeField] private bool m_isMovingDown;

    private float m_startPosition;
    private float m_truePosition;

    private void Start()
    {
        m_startPosition = transform.position.y;
        m_truePosition = transform.position.y;
    }

    private void Update()
    {
        if (m_isMovingDown)
        {
            m_truePosition += m_moveSpeed * Time.deltaTime;
            transform.position = new Vector2(transform.position.x, m_truePosition);

            if (transform.position.y >= m_startPosition + m_distanceBetweenStart)
            {
                m_isMovingDown = false;
            }

            return;
        }

        m_truePosition += -m_moveSpeed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x, m_truePosition);

        if (transform.position.y <= m_startPosition - m_distanceBetweenStart)
        {
            m_isMovingDown = true;
        }
    }
}
