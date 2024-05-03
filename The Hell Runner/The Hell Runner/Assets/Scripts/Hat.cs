using UnityEngine;

public class Hat : MonoBehaviour
{
    private SpriteRenderer m_spriteRenderer;

    [SerializeField] private EventsManager m_events;

    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        m_events.onBoughtCosmetic += SetCosmetic;
    }

    public void SetCosmetic()
    {
        m_spriteRenderer.sprite = CosmeticsManager.m_currentCosmetic;
    }

    private void OnDisable()
    {
        m_events.onBoughtCosmetic -= SetCosmetic;
    }
}
