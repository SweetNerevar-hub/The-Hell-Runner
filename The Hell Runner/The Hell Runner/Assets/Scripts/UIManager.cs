using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private EventsManager m_events;
    [SerializeField] private GameObject m_deathScreen;

    private void Start()
    {
        m_events.onPlayerDeath += DisplayDeathScreen;
    }

    public void DisplayDeathScreen()
    {
        m_deathScreen.SetActive(true);
    }

    private void OnDisable()
    {
        m_events.onPlayerDeath -= DisplayDeathScreen;
    }
}
