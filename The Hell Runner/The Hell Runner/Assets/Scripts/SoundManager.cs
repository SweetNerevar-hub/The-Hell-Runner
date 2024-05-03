using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource m_audio;

    private void Awake()
    {
        if (Instance != null && this != Instance)
        {
            Destroy(this);
        }

        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        m_audio = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        if (m_audio.enabled)
        {
            m_audio.PlayOneShot(clip);
        }
    }
}
