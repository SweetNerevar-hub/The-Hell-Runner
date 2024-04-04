using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventsManager")]
public class EventsManager : ScriptableObject
{
    public event Action onPlayerDeath;

    public void Event_OnPlayerDeath()
    {
        onPlayerDeath?.Invoke();
    }
}
