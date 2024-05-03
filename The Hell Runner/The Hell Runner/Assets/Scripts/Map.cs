using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private EventsManager m_events;
    [SerializeField] private GameManager m_gameManager;

    [SerializeField] private GameObject[] sectors;
    [SerializeField] private List<GameObject> avaliableSectors;
    [SerializeField] private GameObject latestSector;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float m_scrollSpeed;

    private const float m_maxScrollSpeed = 6;
    private float m_trueScrollSpeed;

    [SerializeField] private Transform player;

    Vector2 m_midScreen;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        avaliableSectors = new List<GameObject>(sectors.Length);
    }

    private void Start()
    {
        m_events.onPlayerDeath += StopScrolling;

        CheckForEmptyList();

        m_midScreen = mainCamera.WorldToScreenPoint(mainCamera.transform.position);
        m_trueScrollSpeed = m_scrollSpeed;
    }

    private void Update()
    {
        m_scrollSpeed = ScaleScrollSpeed();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * m_scrollSpeed * Time.deltaTime);
    }

    public void SpawnSector()
    {
        int sector = GetRandomSectorFromList();
        GameObject lastSector = latestSector;

        latestSector = Instantiate(avaliableSectors[sector], transform);
        latestSector.transform.position = lastSector.GetComponent<Sector>().end.position;

        if (m_trueScrollSpeed < m_maxScrollSpeed)
        {
            m_trueScrollSpeed += 0.2f;
        }

        avaliableSectors.RemoveAt(sector);
        CheckForEmptyList();
        m_gameManager.IncreaseTotalSectorsLoaded();
    }

    private void CheckForEmptyList()
    {
        if (avaliableSectors.Count == 0)
        {
            foreach (GameObject sector in sectors)
            {
                avaliableSectors.Add(sector);
            }
        }
    }

    private void StopScrolling()
    {
        enabled = false;
    }

    private void OnDisable()
    {
        m_events.onPlayerDeath -= StopScrolling;
    }

    private float ScaleScrollSpeed()
    {
        Vector2 playerPosOnScreen = mainCamera.WorldToScreenPoint(player.position);
        float speedScaleMultiplier = 0.05f;

        if (playerPosOnScreen.x > m_midScreen.x && m_scrollSpeed < 6)
        {
            return m_scrollSpeed += speedScaleMultiplier;
        }

        else if (m_scrollSpeed > m_trueScrollSpeed)
        {
            return m_scrollSpeed -= speedScaleMultiplier;
        }

        return m_trueScrollSpeed;
    }

    private int GetRandomSectorFromList()
    {
        return Random.Range(0, avaliableSectors.Count);
    }
}
