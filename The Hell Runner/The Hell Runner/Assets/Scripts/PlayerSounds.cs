using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioClip m_jumpSound;
    [SerializeField] private AudioClip m_landSound;
    [SerializeField] private AudioClip m_wallslidingSound;
    [SerializeField] private AudioClip m_runningSound;
    [SerializeField] private AudioClip m_deathSound;

    public void PlayJumpSound()
    {
        SoundManager.Instance.PlaySound(m_jumpSound);
    }

    public void PlayLandSound()
    {
        SoundManager.Instance.PlaySound(m_landSound);
    }

    public void PlayWallSlidingSound()
    {
        SoundManager.Instance.PlaySound(m_wallslidingSound);
    }

    public void PlayRunningSound()
    {
        SoundManager.Instance.PlaySound(m_runningSound);
    }

    public void PlayDeathSound()
    {
        SoundManager.Instance.PlaySound(m_deathSound);
    }
}
