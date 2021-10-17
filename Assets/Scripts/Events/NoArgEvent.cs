using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "No Arg Event", menuName = "Events/No Arg Event", order = 0)]
public class NoArgEvent : ScriptableObject {
    public event UnityAction Callback = delegate {};
    public void FireEvent() {
        if(Callback != null) Callback.Invoke();
    }
}