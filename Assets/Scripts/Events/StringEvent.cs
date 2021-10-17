using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "String Event", menuName = "Events/String Event", order = 0)]
public class StringEvent : ScriptableObject {
    public event UnityAction <string> Callback = delegate {};

    public void FireEvent(string obj) {
        if(Callback != null) Callback.Invoke(obj);
    }
}