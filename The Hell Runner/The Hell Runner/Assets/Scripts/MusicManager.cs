using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource m_audio;

    [SerializeField] private EventsManager m_events;
    [SerializeField] private AudioClip m_playerDeathClip;
    [SerializeField][Range(0, 1)] private float m_maxVolume;

    private void Start()
    {
        m_events.onPlayerDeath += OnPlayerDeath;

        m_audio = GetComponent<AudioSource>();
        m_audio.volume = 0;
        StartCoroutine(FadeInMusic());
    }

    private IEnumerator FadeInMusic()
    {
        while (m_audio.volume < m_maxVolume)
        {
            m_audio.volume += Time.deltaTime / 2;
            yield return null;
        }
    }

    private void OnPlayerDeath()
    {
        if (m_audio.enabled)
        {
            m_audio.Stop();
            m_audio.PlayOneShot(m_playerDeathClip);
        }
    }

    private void OnDisable()
    {
        m_events.onPlayerDeath -= OnPlayerDeath;
    }
} 
