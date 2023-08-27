using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public sealed class EventHandler 
{
    Queue<UnityEvent> eventQueue;
    public EventHandler()
    {
        eventQueue = new Queue<UnityEvent>();
    }

    // Add event to list pending invocation;
    public bool addEvent<T>(UnityEvent ev, System.Func<T> callback)
    {
        try
        {
            eventQueue.Enqueue(ev);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            callback();
            return false;
        }
    }


    // Dequeue an event and invoke it.
    private void pulse()
    {
        UnityEvent ev = eventQueue.Dequeue();
        ev.Invoke();
    }


    ~EventHandler() 
    { 
    
    }


}
