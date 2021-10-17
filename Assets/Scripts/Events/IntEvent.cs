using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Int Event", menuName = "Events/Int Event", order = 0)]
public class IntEvent : ScriptableObject {
    public event UnityAction <int> Callback = delegate {};

    public void FireEvent(int obj) {
        if(Callback != null) Callback.Invoke(obj);
    }

    public void FireEventHiLowWord(int high, int low)
    {
        if(Callback != null) Callback.Invoke(high << 16 | low);

    }
}