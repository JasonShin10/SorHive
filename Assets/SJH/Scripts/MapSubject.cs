using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSubject : MonoBehaviour
{
    private readonly ArrayList _observers = new ArrayList();

    public void Attach(MapObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(MapObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObserver()
    {
        foreach(MapObserver observer in _observers)
        {
            observer.Notify(this);
        }
    }
}
