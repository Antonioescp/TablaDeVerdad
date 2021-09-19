using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Game Event")]
public class GameEvent : GameEventBase
{
    private UnityEvent gameEvent = new UnityEvent();

    // Adds a listener to the event
    public void AddListener(UnityAction listener)
    {
        gameEvent.AddListener(listener);
    }

    public void RemoveListener(UnityAction listener)
    {
        gameEvent.RemoveListener(listener);
    }

    public void Raise()
    {
        gameEvent.Invoke();
    }
}
