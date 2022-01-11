using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static EventManager _instance;
    public static EventManager instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<EventManager>();
            return _instance;
        }
    }

    public delegate void OnEvent(object param);
    public Dictionary<string, OnEvent> eventList = new Dictionary<string, OnEvent>();

    public void AddListener(string key, OnEvent evnt)
    {
        if (!eventList.TryGetValue(key, out OnEvent e))
        {
            eventList.Add(key, evnt);
        }
    }

    public void PostListener(string key, object param)
    {
        if (eventList.TryGetValue(key, out OnEvent e))
        {
            e.Invoke(param);
        }
    }
}
