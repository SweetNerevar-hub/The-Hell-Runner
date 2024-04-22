using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private EventsManager m_events;
    [SerializeField] private LeaderboardManager m_leaderboardManager;
    [SerializeField] private Text m_score;
    [SerializeField] private Image m_pauseScreen;

    [Header("Death Screen UI")]
    [SerializeField] private GameObject m_deathScreen;
    [SerializeField] private Text m_totalScore;
    [SerializeField] private Text m_totalScoreCalc;
    [SerializeField] private InputField m_playerNameField;

    private void Start()
    {
        m_events.onPlayerDeath += DisplayDeathScreen;
    }

    public void UpdateScore(int score, int sectorsLoaded)
    {
        m_score.text = score.ToString();
        m_totalScore.text = (score * sectorsLoaded).ToString();
        m_totalScoreCalc.text = $"({score} x {sectorsLoaded})";
    }

    public void DisplayDeathScreen()
    {
        m_deathScreen.SetActive(true);
        m_score.enabled = false;
        m_pauseScreen.enabled = true;
        m_playerNameField.ActivateInputField();
    }

    public void ToggleGamePause(bool isPaused)
    {
        m_pauseScreen.enabled = isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            return;
        }

        Time.timeScale = 1;
    }

    public void CheckForValidName()
    {
        if (m_playerNameField.text == "")
        {
            print("No name has been given");
            return;
        }

        m_leaderboardManager.AddNewScore(m_playerNameField.text.ToUpper(), int.Parse(m_totalScore.text));
        ClearInputField();
    }

    private void ClearInputField()
    {
        m_playerNameField.enabled = false;
        m_playerNameField.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        m_events.onPlayerDeath -= DisplayDeathScreen;
    }
}
