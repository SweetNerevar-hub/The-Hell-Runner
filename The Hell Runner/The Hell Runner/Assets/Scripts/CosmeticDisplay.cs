using UnityEngine;
using UnityEngine.UI;

public class CosmeticDisplay : MonoBehaviour
{
    [SerializeField] private CosmeticsManager m_cosmeticsManager;
    [SerializeField] private int arrayID;
    [SerializeField] private int cost;

    [SerializeField] private Sprite m_cosmeticSprite;
    [SerializeField] private Button m_cosmeticsButton;
    [SerializeField] private Button m_buyButton;
    [SerializeField] private Text m_costText;

    private void Start()
    {
        m_costText.text = cost.ToString();
        if (CosmeticsManager.m_hatsUnlocked[arrayID])
        {
            m_buyButton.interactable = false;
        }

        if (CosmeticsManager.m_currentCosmetic == m_cosmeticSprite)
        {
            m_cosmeticsButton.interactable = false;
        }
    }

    public void OnClickedBuyButton()
    {
        if (GameManager.m_totalSouls >= cost)
        {
            GameManager.m_totalSouls -= cost;
            m_cosmeticsManager.UnlockNewCosmetic(arrayID);
            m_buyButton.interactable = false;
            return;
        }
    }

    public void OnClickedHatButton()
    {
        m_cosmeticsManager.ChangeCurrentCosmetic(arrayID, m_cosmeticsButton);
    }

    public Sprite GetCosmeticSprite()
    {
        return m_cosmeticSprite;
    }

    public Button GetCosmeticsButton()
    {
        return m_cosmeticsButton;
    }
}
