using UnityEngine;
using UnityEngine.UI;

public class UIAudioToggle : MonoBehaviour
{
    [SerializeField] private AudioSource m_musicManager;
    [SerializeField] private AudioSource m_soundManager;
    [SerializeField] private RawImage m_image;
    [SerializeField] private Texture m_playing;
    [SerializeField] private Texture m_muted;

    public void ToggleAudio()
    {
        m_musicManager.enabled = !m_musicManager.enabled;
        m_soundManager.enabled = !m_soundManager.enabled;

        if (m_soundManager.enabled)
        {
            m_image.texture = m_playing;
            return;
        }

        m_image.texture = m_muted;
    }
}
