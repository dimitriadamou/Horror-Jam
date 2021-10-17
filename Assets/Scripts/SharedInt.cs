using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "Shared Int", menuName = "Horror Jam/Shared Integer", order = 0)]
public class SharedInt : ScriptableObject {
    public event UnityAction <int> Callback = delegate {};


    [SerializeField] private int SerializedValue;
    public int Value {
        set {
            SerializedValue = value;
            Callback.Invoke(value);
        }
        get {
            return SerializedValue;
        }
    }
}