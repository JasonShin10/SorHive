using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapObserver : MonoBehaviour
{
    public abstract void Notify(MapSubject subject);
}
