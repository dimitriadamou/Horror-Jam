using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEditor;

public class SubtitleClip : PlayableAsset
{
    public int startPos;
    public int length;

    public Story story;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<SubtitleBehaviour>.Create(graph);

        SubtitleBehaviour subtitleBehaviour = playable.GetBehaviour();
        subtitleBehaviour.startPos = startPos;
        subtitleBehaviour.length = length;
        return playable;
    }
}
