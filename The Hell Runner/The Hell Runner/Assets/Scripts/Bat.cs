using UnityEngine;

public class Bat : MonoBehaviour
{
    private float m_moveSpeed;
    private float m_freq;
    private float m_distance;
    private float m_startYPos;

    private void Start()
    {
        m_startYPos = GameObject.FindWithTag("Player").transform.position.y;

        m_moveSpeed = RandomFloat();
        m_freq = RandomFloat();
        m_distance = RandomFloat() / 2f;
    }

    private void Update()
    {
        float xPos = transform.position.x - m_moveSpeed * Time.deltaTime;
        float yPos = m_startYPos + Mathf.Sin(Time.time * m_freq) * m_distance;

        transform.position = new Vector2(xPos, yPos);
    }

    private float RandomFloat()
    {
        return Random.Range(1f, 4f);
    }
}
