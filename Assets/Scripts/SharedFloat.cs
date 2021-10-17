using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "Shared Float", menuName = "Horror Jam/Shared Float", order = 0)]
public class SharedFloat : ScriptableObject {
    public event UnityAction <float> Callback = delegate {};
    public float Value {
        set {
            Value = value;
            Callback.Invoke(value);
        }
        get {
            return Value;
        }
    }
}