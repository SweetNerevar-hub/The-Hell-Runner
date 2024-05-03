using UnityEngine;
using UnityEngine.UI;

public class CosmeticsManager : MonoBehaviour
{
    static public Sprite m_currentCosmetic;
    static public bool[] m_hatsUnlocked = new bool[3];

    [SerializeField] private EventsManager m_events;
    [SerializeField] private SceneHandler m_sceneHandler;
    [SerializeField] private CosmeticDisplay[] m_hatSprites;
    [SerializeField] private Text m_soulAmountText;

    public void SetSoulAmountText()
    {
        m_soulAmountText.text = $"Your currently have {GameManager.m_totalSouls} Lost Souls";
    }

    public void UnlockNewCosmetic(int arrayIndex)
    {
        m_hatsUnlocked[arrayIndex] = true;
        m_currentCosmetic = m_hatSprites[arrayIndex].GetCosmeticSprite();

        ToggleCosmeticsButtonInteractables(m_hatSprites[arrayIndex].GetCosmeticsButton());
        SetSoulAmountText();
        m_events.Event_OnBoughtCosmetic();
    }

    public void ChangeCurrentCosmetic(int arrayIndex, Button b)
    {
        if (m_hatsUnlocked[arrayIndex])
        {
            m_currentCosmetic = m_hatSprites[arrayIndex].GetCosmeticSprite();
            ToggleCosmeticsButtonInteractables(b);
        }
    }

    private void ToggleCosmeticsButtonInteractables(Button b)
    {
        for (int i = 0; i < m_hatSprites.Length; i++)
        {
            m_hatSprites[i].GetCosmeticsButton().interactable = true;
        }

        b.interactable = false;
    }
}
