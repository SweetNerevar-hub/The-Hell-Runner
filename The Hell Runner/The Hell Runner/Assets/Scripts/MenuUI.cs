using UnityEngine;

public class MenuUI : MonoBehaviour
{
    private enum MENU_STATE { TITLE, LEADERBOARD }

    [SerializeField] private LeaderboardManager m_leaderboardManager;
    [SerializeField] private GameObject m_titleScreen;
    [SerializeField] private GameObject m_leaderboardScreen;
    
    private MENU_STATE m_menuState;

    public void ToggleMenuStates()
    {
        if (m_menuState == MENU_STATE.TITLE)
        {
            m_menuState = MENU_STATE.LEADERBOARD;
            m_titleScreen.SetActive(false);
            m_leaderboardScreen.SetActive(true);
            m_leaderboardManager.DisplayLeaderboard();
            return;
        }

        m_menuState = MENU_STATE.TITLE;
        m_titleScreen.SetActive(true);
        m_leaderboardScreen.SetActive(false);
    }
}
