using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EventsManager m_events;
    [SerializeField] private UIManager m_uiManager;

    [SerializeField] private AudioClip m_collectSoulSound;

    private bool m_isPaused;
    private bool m_isPlayerDead;
    private int m_currentScore;
    private int m_totalSectorsLoaded;
    private int m_totalScore;

    static public int m_totalSouls = 0;

    private void Start()
    {
        m_events.onPlayerDeath += OnPlayerDeath;
        m_events.onCollectSoul += OnCollectSoul;

        ToggleCursorHide(true);
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

        ToggleCursorHide(false);
    }

    private void OnCollectSoul()
    {
        m_totalSouls++;
        SoundManager.Instance.PlaySound(m_collectSoulSound);
    }

    public void IncreaseTotalSectorsLoaded()
    {
        m_totalSectorsLoaded++;
    }

    public void ToggleCursorHide(bool doLock)
    {
        if (doLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            return;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        m_events.onPlayerDeath -= OnPlayerDeath;
        m_events.onCollectSoul -= OnCollectSoul;
    }
}