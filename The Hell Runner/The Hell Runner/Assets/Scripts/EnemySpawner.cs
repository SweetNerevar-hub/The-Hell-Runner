using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform map;
    [SerializeField] private GameObject bat;

    private float m_timer = 10f;

    private void Update()
    {
        m_timer -= Time.deltaTime;

        if (m_timer <= 0)
        {
            Instantiate(bat, transform.position, Quaternion.identity, map);
            m_timer = RandomTime();
        }
    }

    private float RandomTime()
    {
        return Random.Range(4f, 8f);
    }
}
