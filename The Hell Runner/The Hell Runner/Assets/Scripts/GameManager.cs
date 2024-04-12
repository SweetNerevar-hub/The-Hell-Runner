using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EventsManager m_events;
    [SerializeField] private UIManager m_uiManager;
    [SerializeField] private GameObject bat;

    private bool m_isPaused;
    private bool m_isPlayerDead;
    private int m_currentScore;
    private int m_totalSectorsLoaded;
    private int m_totalScore;

    private void Start()
    {
        m_events.onPlayerDeath += OnPlayerDeath;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !m_isPlayerDead)
        {
            m_isPaused = !m_isPaused;
            m_uiManager.ToggleGamePause(m_isPaused);
        }

        if (!m_isPlayerDead && !m_isPaused)
        {
            m_currentScore++;
            m_uiManager.UpdateScore(m_currentScore, m_totalSectorsLoaded);
        }
    }

    private void OnPlayerDeath()
    {
        m_isPlayerDead = true;
        m_totalScore = m_currentScore * m_totalSectorsLoaded;
    }

    public void IncreaseTotalSectorsLoaded()
    {
        m_totalSectorsLoaded++;
    }

    private void OnDisable()
    {
        m_events.onPlayerDeath -= OnPlayerDeath;
    }
}