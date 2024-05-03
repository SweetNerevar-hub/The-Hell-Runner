using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private LeaderboardManager m_leaderboardManager;
    [SerializeField] private GameObject m_titleScreen;
    [SerializeField] private GameObject m_leaderboardScreen;
    [SerializeField] private GameObject m_shopScreen;

    public void OpenTitleScreen()
    {
        m_titleScreen.SetActive(true);
        m_leaderboardScreen.SetActive(false);
        m_shopScreen.SetActive(false);
    }

    public void OpenLeaderboardScreen()
    {
        m_titleScreen.SetActive(false);
        m_shopScreen.SetActive(false);
        m_leaderboardScreen.SetActive(true);
        m_leaderboardManager.DisplayLeaderboard();
    }

    public void OpenShopScreen()
    {
        m_titleScreen.SetActive(false);
        m_leaderboardScreen.SetActive(false);
        m_shopScreen.SetActive(true);
    }
}
