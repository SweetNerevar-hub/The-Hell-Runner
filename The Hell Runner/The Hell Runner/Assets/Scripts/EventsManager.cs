using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventsManager")]
public class EventsManager : ScriptableObject
{
    public event Action onPlayerDeath;
    public event Action onCollectSoul;
    public event Action onBoughtCosmetic;

    public void Event_OnPlayerDeath()
    {
        onPlayerDeath?.Invoke();
    }

    public void Event_OnCollectSoul()
    {
        onCollectSoul?.Invoke();
    }

    public void Event_OnBoughtCosmetic()
    {
        onBoughtCosmetic?.Invoke();
    }
}
