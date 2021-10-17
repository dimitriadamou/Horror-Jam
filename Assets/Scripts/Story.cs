using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Story", menuName = "Horror Jam/Story", order = 0)]
public class Story : ScriptableObject {
    public List<string> Lines;
    public AudioClip audio;

    public string GetText()
    {
        return string.Join(" ", Lines);
    }
}